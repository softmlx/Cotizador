using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class ContactoCliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid ContactoClienteId { get; set; }
        public string Titulo { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Extension { get; set; }
        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }
        public int Puesto { get; set; }
        [Column(Order = 1)]
        public Guid ClienteId { get; set; }
        public Boolean Estatus { get; set; }
    }
}