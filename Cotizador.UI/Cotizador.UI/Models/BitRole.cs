using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Cotizador.UI.Models
{
    [JsonObject(IsReference = true)]
    public class BitRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BitRoleId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<BitRolePermission> BitRolePermission { get; set; }
        public List<BitUserRole> BitUserRole { get; set; }
    }
}