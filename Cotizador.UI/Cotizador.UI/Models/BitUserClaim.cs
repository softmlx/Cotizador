using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class BitUserClaim
    {
        [Key]
        [Column(Order = 0)]
        public Guid BitUserClaimId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid BitUserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}