using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class TrabajosDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int TrabajoId { get; set; }

    [ForeignKey("TrabajoId")]
    public Trabajos Trabajo { get; set; }

    public int ArticuloId { get; set; }

    [ForeignKey("ArticuloId")]
    public Articulos Articulo { get; set; }

    [Required(ErrorMessage = "Favor Colocar la cantidad.")]
    public int Cantidad { get; set; }
    [Required(ErrorMessage = "Favor Colocar el precio.")]
    public decimal Precio { get; set; }
    [Required(ErrorMessage = "Favor Colocar el costo.")]
    public decimal Costo { get; set; }

}