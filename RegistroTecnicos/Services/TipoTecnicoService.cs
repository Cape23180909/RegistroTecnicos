using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TipoTecnicoService
{
    private readonly Contexto _contexto;
    public TipoTecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }
    // Método Existente
    public async Task<bool> Existe(int tipoTecnicoId)
    {
        return await _contexto
            .TiposTecnicos.AnyAsync(t => t.TipoId == tipoTecnicoId);
    }
    //Metodo el cual Nos Identifica si ese tecnico esta registrado en la base de datos
    public async Task<bool> ExisteNombreTipoTecnico(string descripcion, int id)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.Descripcion.ToLower().Equals(descripcion.ToLower()) && t.TipoId != id);
    }
    // Método Insertar
    private async Task<bool> Insertar(TiposTecnicos tipoTecnicos)
    {
        _contexto.TiposTecnicos.Add(tipoTecnicos);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    // Método Modificar
    private async Task<bool> Modificar(TiposTecnicos tipoTecnico)
    {
        _contexto.TiposTecnicos.Update(tipoTecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    // Método guardar
    public async Task<bool> Guardar(TiposTecnicos tipoTecnico)
    {
        if (!await Existe(tipoTecnico.TipoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);
    }
    // Método eliminar
    public async Task<bool> Eliminar(int id)
    {
        var eliminarArticulo = await _contexto.TiposTecnicos
             .Where(t => t.TipoId == id)
             .ExecuteDeleteAsync();
        return eliminarArticulo > 0;
    }
    // Método buscar
    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoId == id);
    }
    // Método listar
    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}