using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("InformacionComercialProductos")]
    public class InformacionComercialProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid InformacionComercialProductoId { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public double Margen { get; set; }
        public Guid ProveedorId { get; set; }
        public Guid MonedaId { get; set; }
        public Guid ProductoId { get; set; }
        public bool Estatus { get; set; }

        public virtual Product Producto { get; set; }
    }
}