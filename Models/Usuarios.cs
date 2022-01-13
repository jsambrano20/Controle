using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace Controle.Models
{
    public class Usuarios
    {
        private readonly static string _cn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "email invalido")]
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "É necessário preencher o Email")]
        public string Email { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "É necessário preencher o Nome")]
        public string Nome { get; set; }

        [MinLength(6, ErrorMessage = "Senha necessita de até 6 caracteres")]
        [Display(Name = "Senha: ")]
        [Required(ErrorMessage = "É necessário preencher a Senha")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha: ")]
        [NotMapped]
        [Required(ErrorMessage = "Confirmar Senha")]
        [Compare("Senha")]
        public string ConfirmSenha { get; set; }

        public Usuarios()
        {
        }

        public Usuarios(int id, string email, string nome, string senha)
        {
            Id = id;
            Email = email;
            Nome = nome;
            Senha = senha;
        }

        public bool Login()
        {
            bool result = false;
            var sql = "SELECT id_usuarios, Nome, Senha FROM Usuarios WHERE Email = '" + this.Email + "'";

            try
            {
                using (SqlConnection cn = new SqlConnection(_cn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    if (this.Senha == dr["senha"].ToString())
                                    {

                                        this.Id = Convert.ToInt32(dr["id_usuarios"]);
                                        this.Nome = dr["nome"].ToString();
                                        result = true;
                                    }
                                    else if (this.Senha == null || this.Email == null)
                                    {
                                        result = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Falha: " + ex.Message);
                return false;
            }
            return result;
        }

        //public void Registrar()
        //{
        //    var sql = "insert into Usuarios values (@Email, @Nome, @Senha)";
        //    var l = "select Email from usuarios";
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(_cn))
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand(sql, cn))
        //            {
        //                if (this.Email != l)
        //                {
        //                    cmd.Parameters.AddWithValue("@Email", Email);
        //                    cmd.Parameters.AddWithValue("@Nome", Nome);
        //                    cmd.Parameters.AddWithValue("@Senha", Senha);

        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Email = "Email já utilizado" + ex;
        //    }
        //}
    }
}