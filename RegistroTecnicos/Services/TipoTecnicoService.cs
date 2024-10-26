using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TipoTecnicoService(IDbContextFactory<Contexto> DbFactory)
{
    // Método Existente
    public async Task<bool> Existe(int tipoTecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto
            .TiposTecnicos.AnyAsync(t => t.TipoTecnicoId == tipoTecnicoId);
    }
    //Metodo el cual Nos Identifica si ese tecnico esta registrado en la base de datos
    public async Task<bool> ExisteNombreTipoTecnico(string descripcion, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AnyAsync(t => t.Descripcion.ToLower().Equals(descripcion.ToLower()) && t.TipoTecnicoId != id);
    }
    // Método Insertar
    private async Task<bool> Insertar(TiposTecnicos tipoTecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TiposTecnicos.Add(tipoTecnicos);
        return await contexto
            .SaveChangesAsync() > 0;
    }
    // Método Modificar
    private async Task<bool> Modificar(TiposTecnicos tipoTecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TiposTecnicos.Update(tipoTecnico);
        return await contexto
            .SaveChangesAsync() > 0;
    }
    // Método guardar
    public async Task<bool> Guardar(TiposTecnicos tipoTecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (!await Existe(tipoTecnico.TipoTecnicoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);
    }
    // Método eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminarArticulo = await contexto.TiposTecnicos
             .Where(t => t.TipoTecnicoId == id)
             .ExecuteDeleteAsync();
        return eliminarArticulo > 0;
    }
    // Método buscar
    public async Task<TiposTecnicos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == id);
    }
    // Método listar
    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}