using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("DetalleCotizacions")]
    public class DetalleCotizacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DetalleCotizacionId { get; set; }
        public int Cantidad { get; set; }
        public Double Precio { get; set; }
        public Double Costo { get; set; }
        public Double Margen { get; set; }
        public Guid CotizacionId { get; set; }
        public Guid ProductoId { get; set; }

        public virtual Product Producto { get; set; }
    }
}