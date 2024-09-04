using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RegistroTecnicos.Models
{
    public class TiposTecnicos
    {
        [Key]
        public int TipoId { get; set; }
        [Required (ErrorMessage ="Coloca una descripcion") ]
        public string Descripcion { get; set; }

    }
}
