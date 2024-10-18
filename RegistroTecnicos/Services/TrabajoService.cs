using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;
public class TrabajoService
{
    private readonly Contexto _contexto;
    public TrabajoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    //Metodo Existe
    public async Task<bool> Existe(int trabajoId)
    {
        return await _contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajoId);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Trabajos trabajo)
    {
        _contexto.Trabajos.Add(trabajo);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(Trabajos trabajo)
    {
        _contexto.Trabajos.Update(trabajo);
        var modificado = await _contexto.
            SaveChangesAsync() > 0;
        return modificado;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Trabajos trabajo)
    {
        if (!await Existe(trabajo.TrabajoId))
            return await Insertar(trabajo);
        else
            return await Modificar(trabajo);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        var eliminar = await _contexto.Trabajos
            .Where(T => T.TrabajoId == id)
               .Include(t => t.TrabajosDetalle)
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }
    //Metodo Buscar
    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.Clientes)
            .Include(t => t.TrabajosDetalle)
            .FirstOrDefaultAsync(T => T.TrabajoId == id);
    }
    //Metodo Listar
    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _contexto.Trabajos
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
        return await _contexto.Prioridades
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Articulos>> ListarArticulos()
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TrabajosDetalle>> ListarDetalles(int trabajoId)
    {
        // Busca los detalles del trabajo específico
        var detalles = await _contexto.TrabajosDetalles
            .Where(td => td.TrabajoId == trabajoId)
            .ToListAsync();

        return detalles;

    }
    public async Task<List<TrabajosDetalle>> ObtenerDetalles(int trabajoId)
    {
        return await _contexto.TrabajosDetalles
            .AsNoTracking()
            .Where(t => t.TrabajoId == trabajoId)
            .ToListAsync();
    }

    public async Task<bool> EliminarDetalle(TrabajosDetalle detalle)
    {
        // Aquí busca el detalle en la base de datos y elimínalo.
        var trabajoDetalleDb = await _contexto.TrabajosDetalles.FindAsync(detalle.DetalleId); // Asumiendo que tienes un Id en TrabajosDetalle
        if (trabajoDetalleDb != null)
        {
            _contexto.TrabajosDetalles.Remove(trabajoDetalleDb);
            await _contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }
}