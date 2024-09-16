using Data;
using Data.Computers.SelectVMs;
using Data.Helpers;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Text;
using System.Xml.Linq;
using WebInterface.Helpers;
using WebInterface.Models;

namespace WebInterface.Controllers
{
    public class ComputerController : Controller
    {

        public async Task<IActionResult> Index()
        {
            List<ComputersVM> model = await RequestHelper.SendRequestAsync<object, List<ComputersVM>>(URLs.COMPUTERS_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }

        public async Task<IActionResult> ComputerToPDF(int id)
        {
            ViewData["id"] = id;           
            ComputersVM computersVM = await RequestHelper.SendRequestAsync<object, ComputersVM>(URLs.COMPUTERS_ID.Replace("{id}", id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            computersVM.Template = $"<p>Nazwa urz¹dzenia: {computersVM.Name}</p>\r\n<p>Model: {computersVM.ComputerModel}</p>\r\n<p>Status: {computersVM.Status}</p>\r\n<p>Producent: {computersVM.ManufacturerName}</p>\r\n<p>Typ komptuera: {computersVM.ComputerType}</p>";
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM vm = templates.Where(item => item.Name == "Computers").FirstOrDefault();
            ViewData["template"] = vm;
            //computersVM.Template = vm.HTMLContent;
            return View(computersVM);
        }
        [HttpPost]
        public IActionResult ComputerToPDF(ComputersVM vm)
        {
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10 },
                    Out = @"C:\Users\dlamonski\Downloads\test.pdf",
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HtmlContent = vm.Template,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    },
                }
            };
            converter.Convert(doc);
            return RedirectToAction(nameof(Index));
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
