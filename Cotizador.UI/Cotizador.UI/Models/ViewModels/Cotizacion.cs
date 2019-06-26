using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models.ViewModels
{
    public class Cotizacion
    {
        private List<Partida> _partidas = new List<Partida>();

        public Guid? CotizacionId { get; set; }
        public string Proyecto { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ContactoClienteId { get; set; }
        public Guid MonedaId { get; set; }
        public string Fecha { get; set; }
        public List<Partida> Partidas {
            get { return _partidas; }
            set { _partidas = value; }
        }
        public Double Total { get; set; }
        public Double Subtotal { get; set; }
        public Double IVA { get; set; }
    }
}