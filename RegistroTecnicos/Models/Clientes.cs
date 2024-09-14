using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? WhatsApp { get; set; }
} 