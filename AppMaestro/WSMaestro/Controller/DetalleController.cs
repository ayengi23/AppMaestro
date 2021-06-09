using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSMaestro.Controller
{
    public class DetalleController
    {
        private Connect connect = new Connect();
        public string GetPedidosDetalleIdDB(int Id)
        {
            DataSet pedidos = new DataSet();
            string sql = "SELECT  [IdDetalle] ,[Producto],[Cantidad],[Precio],[Subtotal] "
                        + " FROM[dbConnect].[dbo].[PedidoDetalle] "
                        + " WHERE [Active]  = 1 "
                        + " AND [IdPedido]  = @ID"
                        ;

            using (SqlConnection conn = new SqlConnection(connect.Conexion()))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.CommandText = sql;
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = Id;

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
                            return "No data";
                        }

                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return "";
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public string DeletePedidoDetalleDB(int Id,int IdPedido, string User)
        {

            string sql = "EXEC SpDeletePedidoDetalleById @inIdDetalle, @inPedido,@inUser ";

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
                        cmd.Parameters.Add("@inIdDetalle", SqlDbType.Int).Value = Id;
                        cmd.Parameters.Add("@inPedido", SqlDbType.Int).Value = IdPedido;
                        cmd.Parameters.Add("@inUser", SqlDbType.VarChar, 150).Value = User;

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