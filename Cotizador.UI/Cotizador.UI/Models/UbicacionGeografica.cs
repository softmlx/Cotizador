using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class UbicacionGeografica
    {
        bool bandera = true;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid UbicacionGeograficaId { get; set; }
        [Column(Order = 1)]
        public Guid MunicipioId { get; set; }
        [Column(Order = 2)]
        public Guid EstadoId { get; set; }
        [Column(Order = 3)]
        public Guid PaiId { get; set; }
        public Boolean Estatus { get { return bandera; } set { value = bandera; } }
        public virtual ICollection<Direccion> Direccion { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Municipio Municipio { get; set; }

        public virtual Pai Pais { get; set; }
    }
}