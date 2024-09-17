using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;
public class Prioridades
{
    [Key]
    public int PrioridadId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public double? Tiempo { get; set; }
}