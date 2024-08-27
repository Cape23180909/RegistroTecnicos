using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public decimal? Sueldohora { get; set; }
}
