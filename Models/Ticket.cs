using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Controle.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tick { get; set; }
        public string Data { get; set; }
        public string Cliente { get; set; }
        public string Status { get; set; }
        public string Comentario { get; set; }

        public Ticket() { }

        public Ticket(int id, string titulo, string tick, string data, string cliente,
            string status, string comentario)
        {
            Id = id;
            Titulo = titulo;
            Tick = tick;
            Data = data;
            Cliente = cliente;
            Status = status;
            Comentario = comentario;
        }
        public bool gravarTicket()
        {
            Banco banco = new Banco();

            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = "Insert into tb_planilha values (@TITULO, @TICKET, @DATA, @CLIENTE, @STATUS, @COMENTARIO);";
            command.Parameters.Add("@TITULO", SqlDbType.VarChar);
            command.Parameters.Add("@TICKET", SqlDbType.VarChar);
            command.Parameters.Add("@DATA", SqlDbType.DateTime);
            command.Parameters.Add("@CLIENTE", SqlDbType.VarChar);
            command.Parameters.Add("@STATUS", SqlDbType.VarChar);
            command.Parameters.Add("@COMENTARIO", SqlDbType.VarChar);
            command.Parameters[0].Value = Titulo;
            command.Parameters[1].Value = Tick;
            command.Parameters[2].Value = Data;
            command.Parameters[3].Value = Cliente;
            command.Parameters[4].Value = Status;
            command.Parameters[5].Value = Comentario;




            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {

                banco.fecharConexao();
            }
            return true;
        }

        public Ticket consultaTicket(int id)
        {
            Banco bd = new Banco();
            try
            {
                SqlConnection cn = bd.abrirConexao();
                SqlCommand command = new SqlCommand("select * from tb_planilha", cn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32(0) == id)
                    {

                        this.Id = reader.GetInt32(0);
                        Titulo = reader.GetString(1);
                        Tick = reader.GetString(2);
                        Data = reader.GetString(3);
                        Cliente = reader.GetString(4);
                        Status = reader.GetString(5);
                        Comentario = reader.GetString(6);

                        return this;

                    }


                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                bd.fecharConexao();
            }
        }
    }
}