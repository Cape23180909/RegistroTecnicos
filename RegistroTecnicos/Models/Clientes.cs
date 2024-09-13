using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Ingrese un número de WhatsApp válido (formato requerido: 000-000-0000)")]
    public string? WhatsApp { get; set; }

    public Trabajos? Trabajos { get; set; }
}