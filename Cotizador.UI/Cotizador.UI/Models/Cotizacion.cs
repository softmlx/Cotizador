using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Cotizador.UI.Models
{
    [JsonObject(IsReference = true)]
    public class Cotizacion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid CotizacionId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaCotizacion { get; set; }
        public string Proyecto { get; set; }
        public Double TipoCambio { get; set; }
        public int TotalProductos { get; set; }
        public Double Subtotal { get; set; }
        public Double ImporteIva { get; set; }
        public Double ImporteTotal { get; set; }
        public Guid MonedaId { get; set; }
        public Guid ClienteId { get; set; }
        public Boolean Estatus { get; set; }
        public Guid ContactoClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ContactoCliente ContactoCliente { get; set; }

        public virtual Moneda Moneda { get; set; }


    }
}