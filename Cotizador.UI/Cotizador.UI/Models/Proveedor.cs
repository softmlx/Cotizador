using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class Proveedor
    {
        [Key]
        [Column(Order = 0)]
        public Guid ProvedorId { get; set; }
        public string Nombre { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid ContactoProveedorId { get; set; }
        public Boolean Estatus { get; set; }
    }
}