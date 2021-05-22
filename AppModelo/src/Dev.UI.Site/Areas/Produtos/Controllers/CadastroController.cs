using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.UI.Site.Areas.Produtos.Controllers
{
    [Area(nameof(Produtos))]
    [Route("produtos")]
    public class CadastroController: Controller
    {
        [Route("lista")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("busca")]
        public IActionResult Busca()
        {
            return View();
        }

    }
}
