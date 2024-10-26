using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;
public class CotizacionService(IDbContextFactory<Contexto> DbFactory)
{
    //Metodo Existe 
    public async Task<bool> Existe(int cotizacionid)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionid);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Cotizaciones cotizacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Cotizaciones.Add(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(Cotizaciones cotizacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Cotizaciones cotizacion)
    {
        if (!await Existe(cotizacion.CotizacionId))

            return await Insertar(cotizacion);
        else
            return await Modificar(cotizacion);

    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminar = await contexto.Cotizaciones
            .Where(c => c.CotizacionId == id)
               .Include(c => c.cotizacionesDetalles)
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }

    //Metodo Buscar
    public async Task<Cotizaciones?> Buscar(int cotizacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(c => c.CotizacionId)
            .Include(c => c.Clientes)
            .Include(c => c.cotizacionesDetalles)
            .FirstOrDefaultAsync(c => c.CotizacionId == cotizacionId);
    }

    //Metodo Listar
    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .AsNoTracking()
            .Where(criterio)
             .Include(c => c.Clientes)
            .Include(c => c.cotizacionesDetalles)
            .ToListAsync();
    }
    public async Task<Cotizaciones> ObtenerCotizacionesPorId(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(c => c.cotizacionesDetalles) // Incluye detalles de la cotización
            .FirstOrDefaultAsync(c => c.CotizacionId == id); // Cambia 'Id' por el nombre de tu propiedad clave
    }
    public async Task<bool> EliminarDetalle(CotizacionesDetalle detalle)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cotizacionDetalleDb = await contexto.CotizacionesDetalles.FindAsync(detalle.DetalleId);
        if (cotizacionDetalleDb != null)
        {
            contexto.CotizacionesDetalles.Remove(cotizacionDetalleDb);
            await contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }
}