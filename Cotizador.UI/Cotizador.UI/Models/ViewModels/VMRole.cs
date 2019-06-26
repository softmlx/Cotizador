using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models.ViewModels
{
    public class VMRole
    {
        public BitRole role { get; set; }
        public List<BitPermission> permission { get; set; }
    }
}