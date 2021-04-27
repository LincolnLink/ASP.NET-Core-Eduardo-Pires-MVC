using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MinhaDemoMvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaDemoMvc.Controllers
{
    [Route("")]
    [Route("cliente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("inicio")]
        [Route("inicio/{id:int}/{categorias:guid}")]
        public IActionResult Index(int id, Guid categorias)
        {
            var filme = new Filme
            {
                Titulo = "oi",
                DataLancamento = DateTime.Now,
                Genero = null,
                Avalicao = 10,
                Valor = 20000,

            };

            return RedirectToAction("Privacy", filme);
            //return View();
        }

        [Route("privacidade")]
        [Route("politica-privacidade")]
        public IActionResult Privacy(Filme filme)
        {

            if (ModelState.IsValid)
            {

            }

            foreach (var error in ModelState.Values.SelectMany(m => m.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }


            return View("Privacy");

            //return Content("Qualquer coisa");

            //return Json("{'nome':'Lincoln'}");

            //var fileBytes = System.IO.File.ReadAllBytes(@"D:\arquivo.txt");
            //var fileName = "arquivo.txt";
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("erro")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
