using Controle.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Controle.Controllers
{
    public class GraficoController : Controller
    {
        // GET: Grafico
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GraficoColuna()
        {
            Highcharts columnChart = new Highcharts("columnchart");
            Ticket t = new Ticket();

            columnChart.InitChart(new DotNet.Highcharts.Options.Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 3
            });
            columnChart.SetTitle(new Title()
            {
                Text = ""
            });
            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Quantidade"
            });
            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Resultados", Style = "fontWeight: 'bold', fontSize: '25px'" },
                Categories = new[] { " Status " }
            });
            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Quantidade",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });
            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });
            columnChart.SetSeries(new Series[]
            {
                new Series{
                    Name = "Resolvido",
                    Data = new Data(new object[] {Ticket.GetTicket().Where(x => x.Status.ToString() == "Resolvido").Count()})
                },
                new Series()
                {
                    Name = "Encaminhados",
                    Data = new Data(new object[] {Ticket.GetTicket().Where(x => x.Status.ToString() == "Encaminhado").Count()})
                }
            }
            );
            return View(columnChart);
        }
    }
}