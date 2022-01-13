using Controle.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Controle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["ATickets"];

            if (cookie != null)
            {
                return View();
            }
            else
            {

               return RedirectToAction("Index", "Login");
            }

        }

        public ViewResult Tickets(string sortOrder, string filter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Titulo" : "";
            ViewBag.DateSortParm = sortOrder == "Data" ? "Data_1" : "Data";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = filter;
            }

            ViewBag.CurrentFilter = search;

            var Tickets = from s in Ticket.GetTicket()
                          select s;
            if (!String.IsNullOrEmpty(search))
            {
                Tickets = Tickets.Where(s => s.Titulo.Contains(search)
                                       || s.Tick.Contains(search));
            }
            switch (sortOrder)
            {
                case "Titulo":
                    Tickets = Tickets.OrderByDescending(s => s.Titulo);
                    break;
                case "Data":
                    Tickets = Tickets.OrderByDescending(s => s.Data);
                    break;
                default:
                    Tickets = Tickets.OrderBy(s => s.Data);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Tickets.ToPagedList(pageNumber, pageSize));

        }
        //public ActionResult Tickets()
        //{
        //    ViewBag.Title = "Tickets";
        //    ViewBag.Message = "Chamados";

        //    HttpCookie cookie = Request.Cookies["ATickets"];

        //    if (cookie != null)
        //    {
        //        var lista = Ticket.GetTicket();
        //        ViewBag.Lista = lista;

        //        return View();
        //    }
        //    else
        //    {
        //        Response.Redirect("/Login/Index");
        //        return null;
        //    }
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Meus Contatos";


            return View();

        }

        [HttpPost]
        public FileResult Exportar()
        {
            Ticket db = new Ticket();

            List<object> Tickets = (from ticketess in Ticket.GetTicket()
                                    select new[] {          ticketess.Titulo,
                                                            ticketess.Tick,
                                                            ticketess.Data.ToString(),
                                                            ticketess.Cliente.ToString(),
                                                            ticketess.Status.ToString(),
                                                            ticketess.Comentario,
                                                 }).ToList<object>();
            Tickets.Insert(0, new string[6] { "Titulo", "Ticket", "Data", "Cliente", "Status", "Comentario" });

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Tickets.Count; i++)
            {
                string[] ticket = (string[])Tickets[i];
                for (int j = 0; j < ticket.Length; j++)
                {
                    sb.Append(ticket[j] + '-');
                }
                sb.Append("\r\n");
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "export.csv");
        }

    }
}