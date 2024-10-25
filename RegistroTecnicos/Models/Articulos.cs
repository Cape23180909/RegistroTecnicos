using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Articulos
{
    [Key]
    public int ArticuloId { get; set; }
    [Required(ErrorMessage = "Favor colocar una descripcion.")]
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "Favor colocar un costo.")]
    public decimal Costo { get; set; }
    [Required(ErrorMessage = "Favor colocar un precio.")]
    public decimal Precio { get; set; }
    [Required(ErrorMessage = "Favor colocar la existencia del articulo.")]
    public double Existencia { get; set; }

}