using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("InformacionFiscalProveedores")]
    public class InformacionFiscalProveedor
    {
        [Key]
        [Column(Order=0)]
        public Guid InformacionFiscalId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid ProveedorId { get; set; }
    }
}