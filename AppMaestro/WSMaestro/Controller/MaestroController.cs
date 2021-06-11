using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using WSMaestro.Model;

namespace WSMaestro.Controller
{
    public class MaestroController
    {
        private Connect connect = new Connect();

        public string GetAllPedidosDb()
        {
            List<PedidoHeader> pedidos = new List<PedidoHeader>();
            string sql = "SELECT  IdPedido ,NumPedido,FechaPedido,Cliente,Telefono,Direccion "
                       + " FROM [dbConnect].[dbo].[PedidoHeader] "
                       + " WHERE Activo  = 1 ";

            using (SqlConnection con = new SqlConnection(connect.Conexion()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                pedidos.Add(new PedidoHeader
                                {
                                    IdPedido = sdr["IdPedido"].ToString(),
                                    NumPedido = sdr["NumPedido"].ToString(),
                                    FechaPedido = sdr["FechaPedido"].ToString(),
                                    Cliente = sdr["Cliente"].ToString(),
                                    Telefono = sdr["Telefono"].ToString(),
                                    Direccion = sdr["Direccion"].ToString(),
                                    Mensaje = "Ok"
                                });
                            }
                        }
                        con.Close();
                        string jspedidos = JsonConvert.SerializeObject(pedidos);
                        return jspedidos;
                        //return pedidos;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    pedidos.Add(new PedidoHeader
                    {
                        IdPedido = "",
                        NumPedido = "",
                        FechaPedido = "",
                        Cliente = "",
                        Telefono = "",
                        Direccion = "",
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

        public string GetPedidosIdDB(int Id)
        {
            List<PedidoHeader> pedidos = new List<PedidoHeader>();
            string sql = "SELECT  IdPedido ,NumPedido,FechaPedido,Cliente,Telefono,Direccion "
                       + " FROM[dbConnect].[dbo].[PedidoHeader] "
                       + " WHERE [Activo]  = 1 "
                       +" AND [IdPedido]  = @ID"
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
                                pedidos.Add(new PedidoHeader
                                {
                                    IdPedido = sdr["IdPedido"].ToString(),
                                    NumPedido = sdr["NumPedido"].ToString(),
                                    FechaPedido = sdr["FechaPedido"].ToString(),
                                    Cliente = sdr["Cliente"].ToString(),
                                    Telefono = sdr["Telefono"].ToString(),
                                    Direccion = sdr["Direccion"].ToString(),
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
                    pedidos.Add(new PedidoHeader
                    {
                        IdPedido = "",
                        NumPedido = "",
                        FechaPedido = "",
                        Cliente = "",
                        Telefono = "",
                        Direccion = "",
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