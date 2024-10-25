using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

    public class CotizacionesDetalle
    {
        [Key]
        public int DetalleId { get; set; }

        public int CotizacionId { get; set; }

        [ForeignKey("CotizacionId")]
        public Cotizaciones Cotizacion { get; set; }

        [Required(ErrorMessage = "Favor seleccionar un artículo.")]
        public int ArticuloId { get; set; }

        [ForeignKey("ArticuloId")]
        public Articulos Articulo { get; set; }

        [Required(ErrorMessage = "Favor colocar una cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }