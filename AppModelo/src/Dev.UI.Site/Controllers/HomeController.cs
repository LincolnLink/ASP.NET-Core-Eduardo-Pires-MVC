using Dev.UI.Site.Data;
using Dev.UI.Site.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dev.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        public OperacaoService OperacaoService { get; set; }

        public OperacaoService OperacaoService2 { get; set; }

        public HomeController(OperacaoService operacaoService, OperacaoService operacaoService2)
        {
            OperacaoService = operacaoService;
            OperacaoService2 = operacaoService2;
        }

        //private readonly IPedidoRepository _pedidoRepository;

        //public HomeController(IPedidoRepository pedidoRepository)
        //{
        //   _pedidoRepository = pedidoRepository;
        //}

        /*
        // Teste de Injeção de dependencia.
        public string Index()
        {
            return
                "Primeira instância: " + Environment.NewLine +
                OperacaoService.Trandient.OperacaoId + Environment.NewLine +
                OperacaoService.Scoped.OperacaoId + Environment.NewLine +
                OperacaoService.Singleton.OperacaoId + Environment.NewLine +
                OperacaoService.SingletonInstance.OperacaoId + Environment.NewLine +

                Environment.NewLine +
                Environment.NewLine +

                "Segunda instância: " + Environment.NewLine +
                OperacaoService2.Trandient.OperacaoId + Environment.NewLine +
                OperacaoService2.Scoped.OperacaoId + Environment.NewLine +
                OperacaoService2.Singleton.OperacaoId + Environment.NewLine +
                OperacaoService2.SingletonInstance.OperacaoId + Environment.NewLine;

        }
        */

        
        public IActionResult Index([FromServices] IPedidoRepository _pedidoRepository)
        {
            var pedido = _pedidoRepository.ObterPedido();

            return View();
        }
    }
}
