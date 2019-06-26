using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [Table("ConfiguracionSistemas")]
    public class ConfiguracionSistema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //para generar id al azar
        public Guid ConfiguracionSistemaId { get; set; }
        public String UrlBaseImagenes { get; set; }
        public String UrlLogotipoEmpresa { get; set; }
        public String NombreEmpresa { get; set; }
        public String InformacionFiscalEmpresa { get; set; }
        public Boolean Estatus { get; set; }
        public Double Iva { get; set; }
    }
}