using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }
        [Required(ErrorMessage = "Llenar este campo por favor.")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Llenar este campo por favor.")]
        public decimal? Sueldohora { get; set; }
    }
}
