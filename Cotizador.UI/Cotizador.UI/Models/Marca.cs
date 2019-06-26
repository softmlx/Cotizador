using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("Marcas")]
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //para generar id al azar
        public Guid MarcaId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Boolean Estatus { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}