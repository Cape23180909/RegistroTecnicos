using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClienteService(IDbContextFactory<Contexto> DbFactory)
{

    //Metodo el cual Nos Identifica si ese tecnico esta registrado en la base de datos
    public async Task<bool> ExisteNombreCliente(string whatsApp, int id, string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.WhatsApp.ToLower().Equals(whatsApp.ToLower()) || c.Nombres.ToLower().Equals(nombre.ToLower()) && c.ClienteId != id);
    }

    //Metodo Existe
    public async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }

    //Metodo Insertar
    private async Task<bool> Insertar(Clientes cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(Clientes cliente)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }
    //Metodo Guardar
    public async Task<bool> Guardar(Clientes cliente)
    {
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }
    //Metodo Eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminarCliente = await contexto.Clientes
            .Where(c => c.ClienteId == id)
            .ExecuteDeleteAsync();
        return eliminarCliente > 0;
    }
    //Metodo Buscar
    public async Task<Clientes?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }
    //Metodo Listar
    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<List<Clientes>> ListarClientes()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Clientes> BuscarPorNombre(string nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return null;
        }

        // Busca el cliente por nombre utilizando ToLower para hacer una comparación sin distinción de mayúsculas y minúsculas
        var cliente = await contexto.Clientes
            .FirstOrDefaultAsync(c => c.Nombres.ToLower() == nombre.ToLower());

        return cliente;
    }


}