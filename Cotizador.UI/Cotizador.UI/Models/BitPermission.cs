using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Cotizador.UI.Models
{
    [JsonObject(IsReference = true)]
    public class BitPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BitPermissionId { get; set; }
        public string FriendlyName { get; set; }
        public string GroupedBy { get; set; }
        [JsonIgnore]
        public virtual List<BitRolePermission> BitRoles { get; set; } 
    }
}