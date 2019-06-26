using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("Municipios")]
    public class Municipio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid MunicipioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Boolean Estatus { get; set; }
        [Column(Order = 1)]
        public Guid EstadoId { get; set; }

        public virtual ICollection<UbicacionGeografica> UbicacionGeografica { get; set; }
    }
}