using System;

namespace AspNetCoreIdentity.Models
{
    public class ErrorViewModel
    {
        // id da requisição
        //public string RequestId { get; set; }

        // mostra o request se ele existir
        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErroCode { get; set; }

        public string Titulo { get; set; }

        public string Mensagem { get; set; }
    }
}
