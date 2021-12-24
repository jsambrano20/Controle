using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controle.Models;

namespace Controle.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Ticket()
        {
            ViewBag.Title = "Ticket";
            ViewBag.Message = "Cadastro de Tickets";

            return View();
        }

        [HttpPost]
        public void Salvar()
        {
            Ticket Ticket = new Ticket();
            Ticket.Titulo = Request["Nome"];
            Ticket.Tick = Request["Ticket"];
            Ticket.Data = Convert.ToString(Request["Data"]);
            Ticket.Cliente = Request["Status"];
            Ticket.Comentario = Request["Comentario"];

            Ticket.gravarTicket();
            Response.Redirect("/Home/About");
        }
    }
}