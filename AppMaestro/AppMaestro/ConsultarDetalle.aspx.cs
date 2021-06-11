using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppMaestro
{
    public partial class ConsultarDetalle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                lblId.Text = Request.QueryString["Id"];
                obtenerpedidodetalle(Convert.ToInt32(lblId.Text));
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void obtenerpedidodetalle(int Id)
        {
            var url = $"https://localhost:44336/WebService.asmx/GetPedidosDetalleId?Id=" + Id;
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
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvpedidos.Rows[index];
                int Id = Convert.ToInt32(row.Cells[0].Text);

                string Script = " mensaje();";

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", Script, true);
            }     

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void eliminarproducto(int Id)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "Id=" + lblId.Text;
            byte[] data = encoding.GetBytes(postData);
            var url = $"https://localhost:44336/WebService.asmx/DeletePedidoDetalle";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";
            request.ContentLength = data.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
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
                            string dt = JsonConvert.DeserializeObject(responseBody).ToString();

                            string Script = "alert('" + dt + "');";

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", Script, true);


                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
        }
    }
}