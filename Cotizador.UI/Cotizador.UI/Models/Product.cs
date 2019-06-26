using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //para generar id al azar
        [Column(Order = 0)]
        public Guid ProductoId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionCompleta { get; set; }
        public string modelo { get; set; }
        public string NumeroSerie { get; set; }
        public string CodigoBarras { get; set; }
        //[ForeignKey("Categoria")]
        [Column(Order = 1)]
        public Guid CategoriaId { get; set; }
        //[ForeignKey("Marca")]
        [Column(Order = 2)]
        public Guid MarcaId { get; set; }
        //[ForeignKey("UnidadMedida")]
        [Column(Order = 3)]
        public Guid UnidadMedidaId { get; set; }
        public Boolean Estatus { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual InformacionComercialProducto InfoComercial { get; set; }
    }

}
