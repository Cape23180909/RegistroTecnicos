using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Drawing;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadService(IDbContextFactory<Contexto> DbFactory)
{
    private readonly Contexto _contexto;

    public PrioridadService(Contexto contexto)
    {
        _contexto = contexto;
    }
    //Metodo el cual Nos Identifica si esa prioridad si ya esta registrada en la base de datos
    public async Task<bool> ExistePrioridad(string descripcion, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AnyAsync(p => p.Descripcion.ToLower().Equals(descripcion.ToLower()) || p.Descripcion.ToLower().Equals(descripcion.ToLower()) && p.PrioridadId != id);
    }

    //Metodo Existe
    public async Task<bool> Existe(int prioridadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades.AnyAsync(p => p.PrioridadId == prioridadId);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Prioridades prioridad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Prioridades.Add(prioridad);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(Prioridades prioridad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(prioridad);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Prioridades prioridad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(prioridad.PrioridadId))
            return await Insertar(prioridad);
        else
            return await Modificar(prioridad);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminar = await contexto.Prioridades
            .Where(p => p.PrioridadId == id)
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }
    //Metodo Buscar 
    public async Task<Prioridades?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrioridadId == id);
    }
    //Metodo Listar
    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}