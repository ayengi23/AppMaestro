using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppMaestro.Model
{
    public class PedidosHeader
    {
        public int IdPedido { get; set; }
        public int NumPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Cliente { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}