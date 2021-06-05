
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{

    /// <summary>
    /// Cria a classe para receber o valor string pelo construtor
    /// </summary>
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; }

        public PermissaoNecessaria(string permissao)
        {
            Permissao = permissao;
        }
    }


    /// <summary>
    /// Tipa a Interface com a classe acima criada, essa herança precisa de uma ID de uma interface
    /// </summary>
    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requisito)
        {
                       

            // Verifica se o usuario tem o tipo da Claim e o valor da Claim, definido na tabela "dbo.AspNetUserClaims".
            if(context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requisito.Permissao)))
            {
                // Informa que foi sucesso!
                context.Succeed(requisito);
            }

            // Termina a Task
            return Task.CompletedTask;
        }
    }
    
}
