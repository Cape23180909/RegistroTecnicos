using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistroTecnicos.Models;
public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public DateTime? Fecha { get; set; }
    [ForeignKey("Clientes")]
    public int? ClienteId { get; set; }
    [ForeignKey("Tecnicos")]
    public int? TecnicoId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public decimal? Monto { get; set; }

}