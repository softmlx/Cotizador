using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    [JsonObject(IsReference = true)]
    public class BitRolePermission
    {
        [Key]
        [Column(Order=0)]
        public Guid BitPermissionId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid BitRoleId { get; set; }
        public virtual BitRole BitRole{ get; set; }
        public virtual BitPermission BitPermission { get; set; }
    }
}