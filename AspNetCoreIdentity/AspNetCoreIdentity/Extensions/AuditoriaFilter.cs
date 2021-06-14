using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{
    // configura todos os filtros de ação
    public class AuditoriaFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AuditoriaFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context){}

        /// <summary>
        /// Essa ação é depois do request
        /// </summary>        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Ele faz um registro no logger depois que o usurio fez a autenticação.            
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name.ToString() + " Acessou " +
                    context.HttpContext.Request.GetDisplayUrl().ToString();

                _logger.Info(message);
            }
        }
    }
}
