using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models.ViewModels
{
    public class VMTipoCambio
    {
        public List<TipoCambio> tc { get; set; }
        public List<Moneda> ListMoneda1 { get; set; }
        public List<Moneda> ListMoneda2 { get; set; }
    }
}