using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace WSMaestro.Controller
{
    public class MaestroController
    {
        private Connect connect = new Connect();

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
                            string jspedidos = JsonConvert.SerializeObject(pedidos.Tables[0]);
                            return jspedidos;
                        }
                        else
                        {
                            string jspedidos = JsonConvert.SerializeObject("No data");
                            return jspedidos;
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    string jspedidos = JsonConvert.SerializeObject(ex.Message);
                    return jspedidos;
                }
                finally
                {
                    conn.Close();
                }
            }
           
        }

        public string GetPedidosIdDB(int Id)
        {
            DataSet pedidos = new DataSet();
            string sql = "SELECT  [IdPedido] ,[NumPedido],[FechaPedido],[Cliente] "
                       + " FROM[dbConnect].[dbo].[PedidoHeader] "
                       + " WHERE [Activo]  = 1 "
                       +" AND [IdPedido]  = @ID"
                        ;

            using (SqlConnection conn = new SqlConnection(connect.Conexion()))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.CommandText = sql;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = Id;

                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(pedidos);
                        if (pedidos.Tables.Count > 0)
                        {
                            string jspedidos = JsonConvert.SerializeObject(pedidos.Tables[0]);
                            return jspedidos;
                        }
                        else
                        {
                            string jspedidos = JsonConvert.SerializeObject("No data");
                            return jspedidos;
                        }

                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    string jspedidos = JsonConvert.SerializeObject(ex.Message);
                    return jspedidos;
                }
                finally
                {
                    conn.Close();
                }
            }

        }


        public string ModifyPedidoDB(int Id, string Telefono, string Direccion, string User)
        {

            string sql = "EXEC SpUpdatePedidoHeaderById @inIdPedido, @inTelefono,@inDireccion,@inUser ";

            using (SqlConnection conn = new SqlConnection(connect.Conexion()))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add("@inIdPedido", SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add("@inTelefono", SqlDbType.VarChar,8).Value = Telefono;
                        cmd.Parameters.Add("@inDireccion", SqlDbType.VarChar,255).Value = Direccion;
                        cmd.Parameters.Add("@inUser", SqlDbType.VarChar,150).Value = User;

                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    string jspedidos = JsonConvert.SerializeObject("Informacion actualizada");
                    return jspedidos;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    conn.Close();
                    string jspedidos = JsonConvert.SerializeObject(ex.Message);
                    return jspedidos;
                }
                finally
                {
                    conn.Close();
                }
            }
           
        }
    }
}