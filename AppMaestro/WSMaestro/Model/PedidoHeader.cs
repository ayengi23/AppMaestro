using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSMaestro.Model
{
    public class PedidoHeader
    {
        public string IdPedido { get; set; }
        public string NumPedido { get; set; }
        public string FechaPedido { get; set; }
        public string Cliente { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Mensaje { get; set; }
    }
}