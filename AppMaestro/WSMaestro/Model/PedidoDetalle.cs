using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSMaestro.Model
{
    public class PedidoDetalle
    {
        public string IdDetalle { get; set; }
        public string Producto { get; set; }
        public string Cantidad { get; set; }
        public string Precio { get; set; }
        public string SubTotal { get; set; }
        public string Mensaje { get; set; }
    }
}