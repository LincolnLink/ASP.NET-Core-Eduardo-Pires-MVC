using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.UI.Site.Extensions
{
    public class EmailTagHelper: TagHelper
    {

        public string EmailDomain { get; set; } = "desenvolvedor.io";


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //return base.ProcessAsync(context, output);
            // Define uma saia, define o "a" como tagHelp que é a saida.
            output.TagName = "a";

            // Busca o conteudo da tag
            var content = await output.GetChildContentAsync();

            // Gerando uma saida
            var target = content.GetContent() + "@" + EmailDomain;

            // setando os valores
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);

        }
    }
}
