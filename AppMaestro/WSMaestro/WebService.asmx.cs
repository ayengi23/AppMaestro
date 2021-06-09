using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using WSMaestro.Controller;

namespace WSMaestro
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
    MaestroController controller = new MaestroController();
    DetalleController controllerDetalle = new DetalleController();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAllPedidos()
        {           
            string listPedidos = controller.GetAllPedidosDb();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(listPedidos);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPedidosDetalleId(int Id)
        {
            string listPedidosDetalle = controllerDetalle.GetPedidosDetalleIdDB(Id);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(listPedidosDetalle);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPedidosId(int Id)
        {
            string Pedido = controller.GetPedidosIdDB(Id);
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(Pedido);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ModifyPedido(int Id, string Telefono, string Direccion, string User)
        {
            string Pedido = "";
            if (Id != 0)
            {
                if (string.IsNullOrEmpty(Telefono))
                {
                    Telefono = "";
                }
                if (string.IsNullOrEmpty(Direccion))
                {
                    Direccion = "";
                }
                if (string.IsNullOrEmpty(User))
                {
                    User = "";
                }
                Pedido = controller.ModifyPedidoDB(Id, Telefono, Direccion, User);
            }           
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(Pedido);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeletePedidoDetalle(int Id, int IdPedido, string User)
        {
            string Pedido = "";
            if (Id != 0)
            {
                
                if (string.IsNullOrEmpty(User))
                {
                    User = "";
                }
                Pedido = controllerDetalle.DeletePedidoDetalleDB(Id, IdPedido, User);
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(Pedido);
        }
    }
}
