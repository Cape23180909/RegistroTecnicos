using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicoService
    {
        private readonly Contexto Contexto;
        public TecnicoService(Contexto contexto)
        {
            Contexto = contexto;
        }
        //Metodo Existe
        public async Task<bool> Existe(int tecnicoId)
        {
            return await Contexto.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
        }
        //Metodo Insertar
        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            Contexto.Tecnicos.Add(tecnico);
            return await Contexto.SaveChangesAsync() > 0;

        }
        //Metodo Modificar 
        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            Contexto.Tecnicos.Update(tecnico);
            var Modificado = await Contexto.SaveChangesAsync() > 0;
            Contexto.Entry(tecnico).State = EntityState.Detached;
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
            var EliminarTecnico = await Contexto.Tecnicos
                .Where(t => t.TecnicoId == id)
                .ExecuteDeleteAsync();
            return EliminarTecnico > 0;
        }
        //Metodo Buscar 
        public async Task<Tecnicos?> Buscar(int id)
        {
            return await Contexto.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TecnicoId == id);
        }
        //Metodo Listar
        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return await Contexto.Tecnicos
                .Where(criterio)
                .ToListAsync();
        }
    }
}