using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KissLog;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize] //Só tem acesso usuario que está logado
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        //private readonly ILogger<HomeController> _logger;

        //ILogger<HomeController> logger
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous] // Uma exceção, mesmo com bloqueio de autenticação, usuarios não logado consegue ver
        public IActionResult Index()
        {
            _logger.Trace(message:"Usuario acessou a homer");
            return View();
        }
        
        public IActionResult Privacy()
        {
            //throw new Exception("Error");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {

            /*try
            {
                throw new Exception("Aconteceu algo horrivel");
            }
            catch(Exception e)
            {
                _logger.Error(e);
                throw;
            */
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View();
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimEscrever()
        {
            return View();
        }
        
        [ClaimsAuthorize("Produtos","Ler")]
        public IActionResult ClaimsCustom()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if(id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if(id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negativo";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View("Error", modelErro);
        }
    }
}
