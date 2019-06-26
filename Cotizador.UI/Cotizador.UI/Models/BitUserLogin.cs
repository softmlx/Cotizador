using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class BitUserLogin
    {
        [Key]
        [Column(Order = 0)]
        public Guid LoginProvider { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid ProviderKey { get; set; }
        public Guid BitUserId { get; set; }
    }
}