using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class BitUserRole
    {
        [Key]
        [Column(Order = 0)]
        public Guid BitRoleId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid BitUserId { get; set; }

        public BitUser BitUser { get; set; }
        public BitRole BitRole { get; set; }
    }
}