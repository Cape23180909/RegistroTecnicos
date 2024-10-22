using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

    public class Cotizaciones
    {
    [Key]
    public int CotizacionId { get; set; }
    [Required (ErrorMessage ="Favor colocar la Fecha.")]
    public DateTime Fecha { get; set; }
    [Required(ErrorMessage = "Favor seleccionar un cliente.")]
    [ForeignKey("Clientes")]
    public int? ClienteId { get; set; }
    [Required(ErrorMessage = "Favor colocar una observacion.")]
    public string Observacion { get; set; }
    public decimal Monto { get; set; }

    public ICollection<CotizacionesDetalle> cotizacionesDetalles { get; set; }
}