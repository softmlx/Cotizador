using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class TipoCambio
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        public Guid Moneda1Id { get; set; }
        [Key]
        [Required]
        [Column(Order = 1)]
        public Guid Moneda2Id { get; set; }
        [Required]
        public Double Factor { get; set; }

        public virtual Moneda Moneda { get; set; }
        public virtual Moneda Moneda1 { get; set; }
    }
}