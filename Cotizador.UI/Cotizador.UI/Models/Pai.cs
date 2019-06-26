using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizador.UI.Models
{
    public class Pai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid PaiId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe colocar una clave")]
        [StringLength(50)]
        public string Clave { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public Boolean Estatus { get; set; }

        public ICollection<UbicacionGeografica> UbicacionGeografica { get; set; }
    }
}