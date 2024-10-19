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
    public async Task<bool> ActualizarExistencia(int articuloId, double cantidad)
    {
        var articulo = await _contexto.Articulos.FindAsync(articuloId);

        if (articulo != null)
        {
            // Verifica que la cantidad a reducir no sea mayor que la existencia actual
            if (articulo.Existencia >= cantidad)
            {
                articulo.Existencia -= cantidad; // Reducir la existencia
                _contexto.Articulos.Update(articulo);
                await _contexto.SaveChangesAsync();
                return true; // Indica que la operación fue exitosa
            }
            else
            {
                // Opcional: Manejar el caso donde la cantidad a reducir es mayor que la existencia
                // Puedes lanzar una excepción o manejarlo de otra forma
                throw new InvalidOperationException("No hay suficiente existencia para reducir.");
            }
        }
        return false; // Indica que el artículo no fue encontrado
    }


    public async Task<bool> AgregarCantidad(int articuloId, int cantidad)
    {
        // Validar que la cantidad sea positiva
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad a agregar debe ser mayor que cero.");
        }

        // Buscar el artículo por ID
        var articulo = await _contexto.Articulos.FindAsync(articuloId);

        if (articulo != null)
        {
            // Aumentar la cantidad del artículo
            articulo.Existencia += cantidad;

            // Guardar los cambios en la base de datos
            _contexto.Articulos.Update(articulo);
            await _contexto.SaveChangesAsync();
            return true; // Indica que la operación fue exitosa
        }
        return false; // Indica que el artículo no fue encontrado
    }

    public async Task<Articulos> Buscar(int articuloId)
    {
        return await _contexto.Articulos
            .FirstOrDefaultAsync(a => a.ArticuloId == articuloId);
    }


}