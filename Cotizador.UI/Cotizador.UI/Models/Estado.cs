using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("Estadoes")]
    public class Estado
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid EstadoId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Boolean Estatus { get; set; }
        [Column(Order = 1)]
        public Guid PaisId { get; set; }

        public ICollection<UbicacionGeografica> UbicacionGeografica { get; set; }
    }
}