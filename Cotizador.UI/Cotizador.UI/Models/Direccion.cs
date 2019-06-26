using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("Direccions")]
    public class Direccion
    {
        bool bandera = true;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid DireccionId { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }

        [Column(Order = 1)]
        public Guid UbicacionGeograficaId { get; set; }
        public Boolean Estatus { get { return bandera; } set { value = bandera; } }
        public virtual UbicacionGeografica UbicacionGeografica { get; set; }
    }
}