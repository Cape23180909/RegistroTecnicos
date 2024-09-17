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
        var modificado = await _contexto.SaveChangesAsync() > 0;
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
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }
    //Metodo Buscar
    public async Task<Trabajos?> Buscar(int id)
    {
        return await _contexto.Trabajos
            .AsNoTracking()
            .Include(t => t.Clientes)
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
            .ToListAsync();
    }
}