using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("InformacionFiscals")]
    public class InformacionFiscal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid InformacionFiscalId { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        [Column(Order = 1)]
        public Guid DireccionId { get; set; }
        public bool Estatus { get; set; }

        public virtual Direccion Direccion { get; set; }
    }
}