using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.UI.Site.Areas.Vendas.Controllers
{
    [Area("Vendas")]
    [Route("Vendas")]
    public class PedidosController : Controller
    {
        
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("lista")]
        public IActionResult Lista()
        {
            return View();
        }
    }
}
