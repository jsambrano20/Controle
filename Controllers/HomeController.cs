using Controle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "Tickets";
            ViewBag.Message = "Chamados";


            Banco bd = new Banco();
            string sql = "select * from tb_planilha";
            DataTable dt = new DataTable();

            dt = bd.executarConsultaG(sql);


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}