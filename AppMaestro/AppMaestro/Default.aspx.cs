using AppMaestro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppMaestro
{
    public partial class _Default : Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnObtener_Click(object sender, EventArgs e)
        {
            var url = $"https://localhost:44336/WebService.asmx/GetAllPedidos";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            JavaScriptSerializer js = new JavaScriptSerializer();

                            DataTable dt = (DataTable)JsonConvert.DeserializeObject(responseBody, (typeof(DataTable)));

                            gvpedidos.DataSource = dt;
                            gvpedidos.Visible = true;
                            gvpedidos.DataBind();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
        }

        protected void gvpedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Consultar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvpedidos.Rows[index];
                int Id = Convert.ToInt32(row.Cells[0].Text);

                Response.Redirect("~/ConsultarDetalle.aspx?Id=" + Id);
            }

            if (e.CommandName == "Modificar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvpedidos.Rows[index];
                int Id = Convert.ToInt32(row.Cells[0].Text);

                Response.Redirect("~/ModificarPedido.aspx?Id=" + Id);
            }



        }


    }
}