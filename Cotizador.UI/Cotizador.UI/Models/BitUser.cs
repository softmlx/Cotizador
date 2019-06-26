using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class BitUser
    {
        DateTime dateTime = DateTime.Today;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BitUserId { get; set; }
        public string AdditionalInfo{ get; set; }
        public string Email { get; set; }
        public Boolean EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean PhoneNumberConfirmed { get; set; }
        public Boolean TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get { return dateTime; } set { value = dateTime; } }
        public Boolean LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public List<BitUserRole> BitUserRole { get; set; }
    }
}