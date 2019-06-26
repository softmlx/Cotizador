using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Extension { get; set; }
        public string Celular { get; set; }
        [Column(Order = 1)]
        public Guid DireccionId { get; set; }
        public Boolean Estatus { get { return status; } set { value = status; } }
        bool status = true;
        public virtual Direccion Direccion { get; set; }
        public virtual ICollection<ContactoCliente> ContactoCliente { get; set; }
        public virtual ICollection<InformacionFiscal> InformacionFiscal { get; set; }
        public virtual ICollection<Cotizacion> Cotizacion { get; set; }
    }
}