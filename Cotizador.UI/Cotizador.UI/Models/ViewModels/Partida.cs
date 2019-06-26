using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models.ViewModels
{
    public class Partida
    {
        public Guid? DetalleCotizacionId { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public Double? Costo { get; set; }
        public Double? Precio { get; set; }
        public Double? Margen { get; set; }
    }
}