using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Controle
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute("TicketSalvar", "Ticket/Salvar", new { controller = "Ticket", action = "Salvar" });
            
            routes.MapRoute("TicketExcluir", "Ticket/Excluir/:id", new { controller = "Ticket", action = "Excluir", id = 0 });

            routes.MapRoute("TicketAlterar", "Ticket/Alterar/:id", new { controller = "Ticket", action = "Alterar", id = 0 });

            routes.MapRoute("TicketAdicionar", "Ticket/Adicionar", new { controller = "Ticket", action = "Adicionar" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
