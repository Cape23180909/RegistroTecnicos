using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;
public class TrabajoService(IDbContextFactory<Contexto> DbFactory)
{
    //Metodo Existe
    public async Task<bool> Existe(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajoId);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Trabajos trabajo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Trabajos.Add(trabajo);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(Trabajos trabajo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(trabajo);
        return await contexto
            .SaveChangesAsync() > 0;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Trabajos trabajo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(trabajo.TrabajoId))
            return await Insertar(trabajo);
        else
            return await Modificar(trabajo);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminar = await contexto.Trabajos
            .Where(T => T.TrabajoId == id)
               .Include(t => t.TrabajosDetalle)
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }
    //Metodo Buscar
    public async Task<Trabajos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.Clientes)
            .Include(t => t.TrabajosDetalle)
            .FirstOrDefaultAsync(T => T.TrabajoId == id);
    }
    //Metodo Listar
    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Trabajos
            .AsNoTracking()
            .Where(criterio)
            .Include(t => t.Clientes)
            .Include(t => t.Tecnicos)
            .Include(t => t.Prioridades)
            .Include(t => t.TrabajosDetalle)
            .ToListAsync();
    }

    public async Task<List<Prioridades>> ListarPrioridades()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<List<Articulos>> ListarArticulos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<List<TrabajosDetalle>> ListarDetalles(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var detalles = await contexto.TrabajosDetalles
            .Where(td => td.TrabajoId == trabajoId)
            .ToListAsync();

        return detalles;

    }
    public async Task<List<TrabajosDetalle>> ObtenerDetalles(int trabajoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TrabajosDetalles
            .AsNoTracking()
            .Where(t => t.TrabajoId == trabajoId)
            .ToListAsync();
    }
    public async Task<bool> EliminarDetalle(TrabajosDetalle detalle)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        // Aquí busca el detalle en la base de datos y elimínalo.
        var trabajoDetalleDb = await contexto.TrabajosDetalles.FindAsync(detalle.DetalleId); // Asumiendo que tienes un Id en TrabajosDetalle
        if (trabajoDetalleDb != null)
        {
            contexto.TrabajosDetalles.Remove(trabajoDetalleDb);
            await contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }
}