using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
namespace RegistroTecnicos.Services;

public class ArticuloService
{
    private readonly Contexto _contexto;

    public ArticuloService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Articulos>> ListarArticulos()
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    // Método para obtener un artículo por su ID
    public async Task<Articulos?> ObtenerArticuloPorId(int id)
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArticuloId == id);
    }

    // Método para actualizar la existencia de un artículo
    public async Task ActualizarExistencia(int articuloId, double cantidad)
    {
        var articulo = await _contexto.Articulos.FindAsync(articuloId);
        if (articulo != null)
        {
            articulo.Existencia -= cantidad; // Reducir la existencia
            _contexto.Articulos.Update(articulo);
            await _contexto.SaveChangesAsync();
        }
    }

    public async Task AgregarCantidad(int articuloId, int cantidad)
    {
        // Buscar el artículo por ID
        var articulo = await _contexto.Articulos.FindAsync(articuloId);

        if (articulo != null)
        {
            // Aumentar la cantidad del artículo
            articulo.Existencia += cantidad;

            // Guardar los cambios en la base de datos
            await _contexto.SaveChangesAsync();
        }
    }
}