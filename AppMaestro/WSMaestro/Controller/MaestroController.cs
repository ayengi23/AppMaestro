using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace WSMaestro.Controller
{
    public class MaestroController
    {
        Connect connect = new Connect();

        public string GetAllPedidosDb()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            DataSet pedidos = new DataSet();
            SqlConnection cnn;
            cnn = new SqlConnection(connect.Conexion());
            string sql = "SELECT  [IdPedido] ,[NumPedido],[FechaPedido],[Cliente] "
                        + " FROM[dbConnect].[dbo].[PedidoHeader] "
                        + " WHERE [Activo]  = 1 ";

            using (SqlConnection conn = new SqlConnection(connect.Conexion()))
            {
                try
                {
                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn)) 
                    {
                        adapter.Fill(pedidos);
                        if (pedidos.Tables.Count > 0)
                        {
                            string jspedidos = JsonConvert.SerializeObject(pedidos);
                            return jspedidos;
                        }
                        else
                        {
                            return "No data";
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return "";
                }
            }
           
        }
        //public string GetAllPedidos1()
        //{
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    SqlConnection cnn;
        //    cnn = new SqlConnection(connect.Conexion());
        //    string sql = "SELECT  [IdPedido] ,[NumPedido],[FechaPedido],[Cliente] "
        //                + " FROM[dbConnect].[dbo].[PedidoHeader] "
        //                + " WHERE [Activo]  = 1 ";

        //    using (SqlConnection conn = new SqlConnection(connect.Conexion()))
        //    {
        //        SqlTransaction transaction = null;
        //        try
        //        {
        //            conn.Open();
        //            transaction = conn.BeginTransaction();

        //            using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
        //            {
        //                SqlDataReader reader = cmd.ExecuteReader();
        //            }
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            // Attempt to roll back the transaction.
        //            try
        //            {
        //                transaction.Rollback();
        //            }
        //            catch (Exception ex2)
        //            {
        //                // This catch block will handle any errors that may have occurred
        //                // on the server that would cause the rollback to fail, such as
        //                // a closed connection.
        //            }
        //        }
        //    }
        //    //Context.Response.Clear();
        //    //Context.Response.ContentType = "application/json";
        //    //Employee emp = new Employee();
        //    //emp.EmployeeName = "testName";
        //    //Context.Response.Write(js.Serialize(emp));
        //    //return emp;
        //}
    }
}