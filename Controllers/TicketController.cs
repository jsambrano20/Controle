using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controle.Models;
using PagedList;
using PagedList.Mvc;

namespace Controle.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
 
        public ActionResult Adicionar()
        {
            HttpCookie cookie = Request.Cookies["ATickets"];

            if (cookie != null)
            {

                ViewBag.Title = "Tickets";
                ViewBag.Message = "Registrar Tickets";

                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }

        }

        public ActionResult Alterar(int id)
        {
            HttpCookie cookie = Request.Cookies["ATickets"];

            if (cookie != null)
            {
                ViewBag.Title = "Tickets";
                ViewBag.Message = "Alterar Tickets: ";
                var t = new Ticket();

                t.GetTickets(id);

                return View(t);
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }

        public ActionResult Excluir(int id)
        {
            HttpCookie cookie = Request.Cookies["ATickets"];

            if (cookie != null)
            {
                ViewBag.Title = "Tickets";
                ViewBag.Message = "Excluir Tickets";
                Ticket t = new Ticket();
                t.GetTickets(id);
                ViewBag.Ticket = t;

                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }

        [HttpPost]
        public ActionResult Salvar(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Salvar();
                return RedirectToAction("Tickets", "Home");
            }
            else
            {
                if (ticket.Data == null || ticket.Cliente == null || ticket.Status == null)
                {

                    ViewBag.Title = "Tickets";
                    if (Convert.ToInt32("0" + Request["id"]) == 0)
                    {
                        ViewBag.Message = "Adicionar Ticket";
                        ViewBag.Erro = "Dado Não Preenchidos, Favor verificar e inserir novamente";
                        return View("Adicionar");

                    }
                    else
                    {
                        ViewBag.Ticket = ticket;
                        ViewBag.Message = "Alterar Ticket" + ticket.Id;
                        ViewBag.Erro = "Dado Não Preenchidos, Favor verificar e inserir novamente";
                        return View("Alterar");
                    }

                }
                else
                {
                    ViewBag.Title = "Tickets";
                    if (Convert.ToInt32("0" + Request["id"]) == 0)
                    {
                        ViewBag.Message = "Adicionar Ticket";
                        return View("Adicionar");

                    }
                    else
                    {
                        ViewBag.Ticket = ticket;
                        ViewBag.Message = "Alterar Ticket" + ticket.Id;
                        return View("Alterar");
                    }
                }
            }


        }

        [HttpPost]
        public void Excluir()
        {
            HttpCookie cookie = Request.Cookies["ATickets"];

            if (cookie != null)
            {
                Ticket t = new Ticket();

                t.Id = Convert.ToInt32("0" + Request["id"]);

                t.Excluir();

                Response.Redirect("/Home/Tickets");
            }
            else
            {
                Response.Redirect("/Login/Index");
            }

        }


    }
}


