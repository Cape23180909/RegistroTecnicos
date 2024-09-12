using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace RegistroTecnicos.Services;

public class ClienteService
{
    private readonly Contexto _contexto;

    public ClienteService(Contexto contexto)
    {
        _contexto = contexto;
    }

    //Metodo el cual Nos Identifica si ese tecnico esta registrado en la base de datos
    public async Task<bool> ExisteNombreCliente(string whatsApp, int id, string nombre)
    {
        return await _contexto.Clientes
            .AnyAsync(c => c.WhatsApp.ToLower().Equals(whatsApp.ToLower()) || c.Nombres.ToLower().Equals(nombre.ToLower()) && c.ClienteId != id);
    }

    //Metodo Existe
    public async Task<bool> Existe(int clienteId)
    {
        return await _contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }
    //Metodo Insertar
    private async Task<bool> Insertar(Clientes cliente)
    {
        _contexto.Clientes.Add(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }
    //Metodo Modificar
    private async Task<bool> Modificar(Clientes cliente)
    {
        _contexto.Clientes.Update(cliente);
        var modificado = await _contexto.SaveChangesAsync() > 0;
        return modificado;
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
        var eliminarCliente = await _contexto.Clientes
            .Where(c => c.ClienteId == id)
            .ExecuteDeleteAsync();
        return eliminarCliente > 0;
    }
    //Metodo Buscar
    public async Task<Clientes?> Buscar(int id)
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }
    //Metodo Listar
    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<List<Clientes>> ListarClientes()
    {
        return await _contexto.Clientes
            .AsNoTracking()
            .ToListAsync();
    }
}