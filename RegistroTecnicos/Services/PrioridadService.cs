using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class PrioridadService
    {
        private readonly Contexto _contexto;

        public PrioridadService(Contexto contexto)
        {
            _contexto = contexto;
        }
        //Metodo Existe
        public async Task <bool> Existe (int prioridadId)
        {
            return await _contexto.Prioridades.AnyAsync(p =>p.PrioridadId == prioridadId);
        }
        //Metodo Insertar
        private async Task <bool> Insertar (Prioridades prioridad)
        {
            _contexto.Prioridades.Add(prioridad);
            return await _contexto.SaveChangesAsync() >0;
        }
        //Metodo Modificar
        private async Task <bool> Modificar (Prioridades prioridad)
        {
            _contexto.Prioridades.Update(prioridad);
            var modificado = await _contexto.SaveChangesAsync() >0;
            return modificado;
        }
        //Metodo Guardar
        public async Task <bool> Guardar (Prioridades prioridad)
        {
            if (!await Existe (prioridad.PrioridadId))
                return await Insertar (prioridad);
            else
                return await Modificar (prioridad);
        }
        //Metodo Eliminar
        public async Task <bool> Eliminar (int id)
        {
            var eliminar = await _contexto.Prioridades
                .Where(p =>p.PrioridadId==id)
                .ExecuteDeleteAsync ();
            return eliminar > 0;
        }
        //Metodo Buscar 
        public async Task <Prioridades?> Buscar (int id)
        {
            return await _contexto.Prioridades
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>p.PrioridadId == id);
        }
        //Metodo Listar
        public async Task <List<Prioridades>> Listar (Expression<Func<Prioridades, bool>> criterio)
        {
            return await _contexto.Prioridades
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}