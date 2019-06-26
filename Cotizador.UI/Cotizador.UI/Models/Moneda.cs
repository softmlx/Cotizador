using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class Moneda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid MonedaId { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Simbolo { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public Double Valor { get; set; }
        [Required]
        public Boolean Estatus { get; set; }
        public virtual ICollection<Cotizacion> Cotizacion { get; set; }
        public virtual ICollection<TipoCambio> TipoCambioOrigen { get; set; }
        public virtual ICollection<TipoCambio> TipoCambioDestino { get; set; }

        public virtual ICollection<TipoCambio> TipoCambio { get; set; }
        public virtual ICollection<TipoCambio> TipoCambio1 { get; set; }
    }
}