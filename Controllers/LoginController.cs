using Controle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["ATickets"];

            if (cookie == null)
            {
                if (Session["Erro"] != null)
                    ViewBag.Erro = Session["Erro"].ToString();

                return View();
            }
            else
            {
                Response.Redirect("/Home/Index");
                return null;
            }
        }

        [HttpPost]
        public ActionResult ChecarLogin()
        {
            Usuarios usuario = new Usuarios();
            usuario.Email = Request["Email"];
            usuario.Senha = Request["PassWord"];

            if (usuario.Login())
            {
                SalvarCookie("ATickets", Request["Email"]);
                Session.Remove("Erro");
                return RedirectToAction("Index", "Home");

            }
            else
            {
                Session["Erro"] = "Senha ou Usuário Invalidos";
                return RedirectToAction("Index", "Login");
            }

        }

        //[HttpPost]
        //public ActionResult Salvar(Usuarios usuarios)
        //{
        //    usuarios.Login();
        //    usuarios.Email = Request["Email"];

        //    if (ModelState.IsValid)
        //    {
        //        usuarios.Registrar();
        //        return RedirectToAction("Index", "Login");

        //    }
        //    else
        //    {
        //        if (usuarios.Email == null || usuarios.Nome == null || usuarios.Senha == null)
        //        {
        //            ViewBag.Message = "Registrar Analista";
        //            ViewBag.Erro = "Dados invalidos";
        //            return View("Registrar");

        //        }
        //        else if (usuarios.Senha != usuarios.ConfirmSenha)
        //        {
        //            ViewBag.Message = "Registrar Analista";
        //            ViewBag.Erro = "Senhas não correspondem";
        //            return View("Registrar");
        //        }
        //        else
        //        {
        //            ViewBag.Erro = "Dados Invalidos";
        //            return View("Registrar");
        //        }
        //    }

        //}

        //public ActionResult Registrar()
        //{
        //    ViewBag.Title = "Registrar";
        //    ViewBag.Message = "Registrar Analista";

        //    return View();

        //}

        private void SalvarCookie(string nomeCookie, string user)
        {
            HttpCookie cookie = new HttpCookie(nomeCookie);

            cookie.Values.Add("Usuario", user);

            cookie.Expires = DateTime.Now.AddHours(10);

            cookie.HttpOnly = true;

            Response.AppendCookie(cookie);

        }

    }
}
