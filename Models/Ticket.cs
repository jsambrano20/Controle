using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Controle.Models
{
    public class Ticket
    {
        private readonly static string _cn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public int Id { get; set; }

        [Display(Name = "Titulo: ")]
        [Required(ErrorMessage = "É necessário preencher o Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Ticket: ")]
        [Required(ErrorMessage = "É necessário preencher o Ticket")]
        public string Tick { get; set; }

        [Display(Name = "Data: ")]
        [Required(ErrorMessage = "É necessário inserir a Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Cliente: ")]
        [Required(ErrorMessage = "É necessário inserir um Cliente")]
        public Cliente Cliente { get; set; }

        [Display(Name = "Status: ")]
        [Required(ErrorMessage = "É necessário inserir um Status")]
        public Status Status { get; set; }

        [Display(Name = "Comentario: ")]
        [Required(ErrorMessage = "É necessário preencher o Comentario")]
        public string Comentario { get; set; }


        public Ticket()
        {
        }

        public Ticket(int id, string titulo, string tick, DateTime data, Cliente cliente,
            Status status, string comentario)
        {
            Id = id;
            Titulo = titulo;
            Tick = tick;
            Data = data;
            Cliente = cliente;
            Status = status;
            Comentario = comentario;
        }


        public static List<Ticket> GetTicket()
        {
            var listaTickets = new List<Ticket>();
            var sql = "SELECT * FROM tb_planilha";

            try
            {

                using (SqlConnection cn = new SqlConnection(_cn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaTickets.Add(new Ticket(
                                        Convert.ToInt32(dr["Id"]),
                                        dr["Titulo"].ToString(),
                                        dr["Ticket"].ToString(),
                                        Convert.ToDateTime(dr["Data"]),
                                        (Cliente)Convert.ToByte(dr["Cliente"]),
                                        (Status)Convert.ToByte(dr["Status"]),
                                        dr["Comentario"].ToString()));
                                }


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message); ;
            }
            return listaTickets;

        }

        public void Salvar()
        {

            var sql = "";
            if (Id == 0)
                sql = "insert into tb_planilha values (@TITULO, @TICKET, @DATA, @CLIENTE, @STATUS, @COMENTARIO)";
            else
                sql = "update tb_planilha set titulo=@TITULO, ticket=@TICKET, data=@DATA, cliente=@CLIENTE, status=@STATUS, comentario=@COMENTARIO where id=" + Id;
            try
            {
                using (SqlConnection cn = new SqlConnection(_cn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@Titulo", Titulo);
                        cmd.Parameters.AddWithValue("@Ticket", Tick);
                        cmd.Parameters.AddWithValue("@Data", Data);
                        cmd.Parameters.AddWithValue("@Cliente", Cliente);
                        cmd.Parameters.AddWithValue("@Status", Status);
                        cmd.Parameters.AddWithValue("@Comentario", Comentario);

                        cmd.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Falha: " + ex); ;
            }

        }
        public void GetTickets(int id)
        {
            var sql = "select * from tb_planilha where id =" + id;

            try
            {
                using (SqlConnection cn = new SqlConnection(_cn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    Id = id;
                                    Titulo = dr["Titulo"].ToString();
                                    Tick = dr["Ticket"].ToString();
                                    Data = Convert.ToDateTime(dr["Data"]);
                                    Cliente = (Cliente)Convert.ToByte(dr["Cliente"]);
                                    Status = (Status)Convert.ToByte(dr["Status"]);
                                    Comentario = dr["Comentario"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Titulo = "Falha: " + ex.Message;
                Console.WriteLine("Falha: " + ex.Message);
            }

        }

        public void Excluir()
        {
            var sql = "delete from tb_planilha where id = " + Id;

            try
            {
                using (SqlConnection cn = new SqlConnection(_cn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Falha: " + ex.Message);
            }
        }




    }


    public enum Status : int
    {
        Resolvido = 1,
        Encaminhado = 2,
        Pendente = 3,
    }
    public enum Cliente : int
    {
        UFN = 1,
        ATOS = 2,
        MICROSOFT = 3,
    }

}
