using AppMaestro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppMaestro
{
    public partial class ModificarPedido : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
             if (Request.QueryString["Id"] != null)
                {
                    lblId.Text = Request.QueryString["Id"];
                    obtenerpedidoId(Convert.ToInt32(lblId.Text));
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
        }

        protected void obtenerpedidoId(int Id)
        {
            var url = $"https://localhost:44336/WebService.asmx/GetPedidosId?Id=" + Id;
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

                            this.txtNumPedido.Text = dt.Rows[0][1].ToString();
                            this.txtFechaPedido.Text = dt.Rows[0][2].ToString();
                            this.txtCliente.Text = dt.Rows[0][3].ToString();
                            this.txtTelefono.Text = dt.Rows[0][4].ToString();
                            this.txtDireccion.Text = dt.Rows[0][5].ToString();

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "Id="+lblId.Text + "&Telefono=" + txtTelefono.Text + "&Direccion=" + txtDireccion.Text + "&User=Julio";
            byte[] data = encoding.GetBytes(postData);
            var url = $"https://localhost:44336/WebService.asmx/ModifyPedido";
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

                            string Script = "alert('"+ dt + "');";

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