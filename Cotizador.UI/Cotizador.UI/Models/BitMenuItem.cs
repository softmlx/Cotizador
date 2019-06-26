using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class BitMenuItem
    {
        [Key]
        [Column(Order = 0)]
        public Guid BitMenuItemId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string PathToResource { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid ParentId { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid BitPermissionId { get; set; }
    }
}