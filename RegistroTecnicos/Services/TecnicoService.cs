using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoService
{
    private readonly Contexto _contexto;
    public TecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }
    //Metodo Existe
    public async Task<bool> Existe(int id)
    {
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == id);
    }
    //Metodo el cual Nos Identifica si ese tecnico esta registrado en la base de datos
    public async Task<bool> ExisteNombreTecnico( string nombre, int id)
    {
        return await _contexto.Tecnicos
            .AnyAsync(t => t.Nombres.ToLower ().Equals(nombre.ToLower()) && t.TecnicoId != id);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar 
    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Update(tecnico);
        var Modificado = await _contexto.SaveChangesAsync() > 0;
        return Modificado;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))

            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }
    //Metodo Eliminar 
    public async Task<bool> Eliminar(int id)
    {
        var EliminarTecnico = await _contexto.Tecnicos
            .Where(t => t.TecnicoId == id)
            .ExecuteDeleteAsync();
        return EliminarTecnico > 0;
    }
    //Metodo Buscar 
    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == id);
    }
    //Metodo Listar
    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}