using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WSMaestro.Model;

namespace WSMaestro.Controller
{
    public class DetalleController
    {
        private Connect connect = new Connect();
        public string GetPedidosDetalleIdDB(int Id)
        {
            List<PedidoDetalle> pedidos = new List<PedidoDetalle>();
            string sql = "SELECT  [IdDetalle] ,[Producto],[Cantidad],[Precio],[Subtotal] "
                        + " FROM[dbConnect].[dbo].[PedidoDetalle] "
                        + " WHERE [Active]  = 1 "
                        + " AND [IdPedido]  = @ID"
                        ;

            using (SqlConnection con = new SqlConnection(connect.Conexion()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Id;

                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                pedidos.Add(new PedidoDetalle
                                {
                                    IdDetalle = sdr["IdDetalle"].ToString(),
                                    Producto = sdr["Producto"].ToString(),
                                    Cantidad = sdr["Cantidad"].ToString(),
                                    Precio = sdr["Precio"].ToString(),
                                    SubTotal = sdr["SubTotal"].ToString(),
                                    Mensaje = "Ok"
                                });
                            }
                        }
                        con.Close();
                        string jspedidos = JsonConvert.SerializeObject(pedidos);
                        return jspedidos;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    pedidos.Add(new PedidoDetalle
                    {
                        IdDetalle = "",
                        Producto = "",
                        Cantidad = "",
                        Precio = "",
                        SubTotal = "",
                        Mensaje = ex.Message
                    });

                    string jspedidos = JsonConvert.SerializeObject(pedidos);
                    return jspedidos;
                }
                finally
                {
                    con.Close();
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