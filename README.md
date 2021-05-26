# ASP.NET-Core-Eduardo-Pires-MVC
Aulas de MVC com Eduardo Pires, asp.net CORE


# O padrão MVC

- Ele é um padrão arquitetural

- Se para responsabilidade

# Rotas

- Rotas são basicamente estruturas de navegação personalizadas para que a URL da aplicação possua determinado padrão e atenda as necessidades de passagem de parâmetros.

<blockquete>

    controller=Home / action=Index / {id?}

</blockquete>

- Caso o controller seja nulo ele asume o valor de "Home" e action o valor de "Index".

### Rotas por atributos

- Rotas por atributos é uma maneira alternativa de trabalhar com rotas, são muito mais flexíveis e fáceis de personalizar. As rotas valem apenas para Controller qual foi configurada.

<blockquete>

    [Route("")]
    [Route("Home")]
    [Route("Home/Index")]
    public IActionResult Index(){
        return View();
    }

<blockquete>

# Action Results

- Uma controller pode retornar uma view, e outros resultados

- No asp.net core um Action Result é o tipo de retorno da action da controller, é ultilizada a interface IactionResult que pdoe retornar alguns tipos de resultados:

<blockquete>

JsonResult, PartialViewResult, ViewResult, ViewComponentResult, etc

<blockquete>

# Verbos HTTP

- GET: pedindo informação ao servidor

- POST: envia informação

- PUT: envia para atualizar

- DELETE: envia o id para deletar


# Entendendo o padrão MVC na prática

- Cria um projeto MVC

- você pode configurar uma compatibilidade de versão no arquivo "StartUp" no método "ConfiguraService".

<blockquete>

    services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

<blockquete>

- depois do "AddMvc" ser criado ele deve ser configurado no método "Configure"!

- Na configuração está diferente porq a video aula mostra dessa forma por está no aspnet core 2.2

<blockquete>
    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
    });
<blockquete>

- No projeto que foi gerado mostra de uma forma diferente.

<blockquete>
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
<blockquete>

- Com a rota configurada, podemos executar!

# O vinculo da view com os métodos do controller!

 - Para criar esse vinculo a view deve ter o mesmo nome do método do controller que retorna uma "IActionResult"

 - O método que o método do controller retorna, deternima o retorno do "IActionResult"!

 - O método "view()" define que sera retornado uma "ViewResult".

 - Caso o nome do método do "IActionResult" seja diferente da view, você deve definir dentro do método(as vezes não funciona, tenha preferencia de por o mesmo nome)

<blockquete>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacys()
        {
            return View("Privacy");
        }
    }
<blockquete>

# Dicionario de rotas(endpoints)

- A rota padrão deve ser a ultima, para ter a chance de avaliar todas.

- Criando outra rotas, na configuração de rotas no arquivo "StartUp".


<blockquete>

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "modulos",
            pattern: "Gestao/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
            name: "categoria",
            pattern: "{controller=Home}/{action=Index}/{id}/{categoria?}");


        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

    });

</blockquete>

- Na configuração de rotas no arquivo "StartUp" não precisa difinir o tipo do parametro.

- Testando a passagem de parametro.

<blockquete> ht tp s://localhost:44367/Gestao/Home/Index/10 </blockquete>

- O método "Index" que retorna o "IActionResult" recebe o valor 10 que foi passado na rota.

- Caso tire o "gestão", o sistema reconhece a outra rota que foi configurada

<blockquete> ht tps: //localhost:44367/Home/Index/10/teste </blockquete>

- Para forçar parametros que Não ESTÃO CONFIGURADO nas rotas, mas está sendo recebido no action como parametro.

- Devese por a variavel e por o valor:

<blockquete> https://localhost:44367/Gestao/Home/Index/10/?categorias=teste </blockquete>


# Implementando rotas inteligentes (rotas de atributos)

- Comentei as rotas criadas, e deixei a padrão.

- Tratabalhando com rotas na controller 

- Metadados "[Routes()]" que renomeia controllers e actions.

<blockquete>

    [Route("cliente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("inicio")]
        public IActionResult Index(string id, string categorias)
        {
            return View();
        }
    }
</blockquete>

- Com isso o caminho para acessar a pagina deve ser 

<blockquete> https://localhost:44367/cliente/inicio </blockquete>

- Pode passar parametros usando essas rotas de atributos

- Assim é definido como a rota recebe parametro e passa para o método, sempre use o mesmo nome dos parametros.

<blockquete>

    [Route("")]
    [Route("inicio")]        
    [Route("inicio/{id}/{categorias?}")]
    public IActionResult Index(string id, string categorias)
    {
        return View();
    }

</blockquete>

- Podemos definir o tipo do parametro

# sobreCarga de rotas

- Pode por dois nomes para definir a mesma rota

<blockquete>

    [Route("privacidade")]
    [Route("politica-privacidade")]
    public IActionResult Privacy()
    {
        return View("Privacy");
    }

</blockquete>

- Definir o tipo de parametro ajuda na segurança !

#  Trabalhando com Action Results

- IActionResults é uma interface que retorna algo de forma asyncrona! 

- O nome da IActionResults é sempre o nome do arquivo(da view).

- Retornando outros formatos

<blockquete>

        [Route("privacidade")]
        [Route("politica-privacidade")]
        public IActionResult Privacy()
        {            
            return Json("{'nome':'Lincoln'}");
        }

</blockquete>

- Criando um exemplo de um retorno de download de arquivo

<blockquete>

    [Route("privacidade")]
    [Route("politica-privacidade")]
    public IActionResult Privacy()
    {
        

        var fileBytes = System.IO.File.ReadAllBytes(@"D:\arquivo.txt");
        var fileName = "arquivo.txt";
        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

    }
</blockquete>

# Models

- No MVC um modelo é a representação de um objeto do mundo real, na maioria das vezes representa uma tabela de um banco de dados.

- Pode ser um conjunto de informações de diversos objetos em um só, conhecido como DTO, que são ultilizados para diminuir o número de requisições do servidor.

- Com os annotations, vc define o preenchimento correto dos dados, as validações, e o mapeamento.

# Trabalhando com Models

    - Exemplo:

    <blockquete>

        public class Filme
        {
            public int Id { get; set; }            
            public string Titulo { get; set; }
            public DateTime DataLancamento { get; set; }
            public string Genero { get; set; }
            public decimal Valor { get; set; }
            public int Avalicao { get; set; }
        }
    <blockquete>

# Trabalhando com DataAnnotations

 - Required: define que é obrigatorio e pode definir uma mensagem de erro.

 <blockquete>
    [Required(ErrorMessage = "O campo Título é obrigatório")]
 </blockquete>

 - stringLength: limites de caracteres

 <blockquete>
    [StringLength(30,MinimumLength = 3)]

    [StringLength(30,MinimumLength = 3, ErrorMessage = "" +
            "O maximo de caracteres é 30 e o minimo é 3")]
 </blockquete>

- DataType: Validando data

<blockquete>
 [DataType(DataType.DateTime, ErrorMessageResourceName = "Data em formato incorreto")]      

</blockquete>

- Display: define o nome que aparece na tela

<blockquete>
    [Display(Name = "Data de Lançamento")]
</blockquete>

- RegularExpression: 
<blockquete>
    [RegularExpression(@"^[A-Z]+[a-zA-Z\u00C0-\u00FF""'\w-]*$")]

    [RegularExpression(@"^[0-5]*$", ErrorMessage ="Somente números de 0 á 5")]
       
</blockquete>

- Range
<blockquete>
 [Range(1,1000, ErrorMessage ="Minimo 1 a 1000")]
</blockquete>

- Column: como vai ser a coluna do banco de dados, dessa propriedade.
<blockquete>
[Column(TypeName = "decimal(18,2)")]
</blockquete>

- Key: informa que é uma chave primaria.

<blockquete>
[Key]
</blockquete>

- SobreCargas: pode criar duas validações no mesmo [], mais de uma anotação é passada na mesma linha.

<blockquete>
[StringLength(30, ErrorMessage = "Maximo 30 caracteres"),
        Required(ErrorMessage = "O campo Título é obrigatório")]
</blockquete>


# Validação de Modelos

 - SelectMany (método do LinQ): seleciona apenas os erros nesses valores, é uma coleção dentro de uma coleção.

 - Redirecionando rotas com 

<blockquete>
    return RedirectToAction("Privacy", filme);
</blockquete>

<blockquete>
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
            }
</blockquete>

# Apresentando o Razor

- No MVC o motor de renderização das Views chama-se Razor, logo nós temos as Razor Views que são arquivos HTML mesclados com recursos do Razor.

- Os recursos do Razor servem para criar uma experiência mais rica no desenvolvimento de páginas HTMl. Podemos pensar em views fortemente tipadas.

- Omecanismo Razortransforma as Views em arquivos HTML puros para a interpretação do Borwser.


# Conhecendo os TagHelpers

- Tag Helpers são os recursos do Razor para geração de HTML. No ASP.NET MVC5 este recurso chama-se HTML Helpers e são muito similares.

- A grande melhoria nos Tag Helpers é a nova sintaxe muito mais agradável e próxima do HTML

<blockquete>
 < label asp-for="password" class="..">
    < span asp-validation-for="password">
</blockquete>

# Views de configuração

- pasta shared: com tem as view, que vai ser usada em toda a aplicação.

- _ViewStart: informa qual pagina sera a principal da palicação.

- _ViewImports: importa component, que será usado de forma global.

- _Layout: pagina mestre.

# Apresentando as Partial Views

- Partial Views são pedaçoes de uma view que possuem dados e que podem ser reaproveitados em N views, assim proporcionando mais reaproveitamento de código.

- As partial views dependem do modelo implementado na view principal, gerando certa limitação no seu uso.

As partial view são muito ultilizadas também para rederizar dinamente parte de uma view através de requisições AJAX.

# Apresentando os View Components

- View Components é um novo recurso do ASP.NET MVC Core, é um poderoso aliado para desenvolvimento de componentes indepentes das views.

- Os view Components possuem processamento serve-side independente e podem realizar ações como obter dados de uma tabela e exibir valores manipulados.

- É uma excelente funcionalidade para componentizar recursos de página como um carrinho de compra por ex. 

# Trabalhando com Views na prática

- Toda view pode eleger uma view de Layout. testa


- @RenderBody() : renderiza o conteudo, do layoutMestre.

- @RenderSection("script", requied: false): executa o script antes da dependencia.

# Trabalhando com Partial Views

- Renderizar é ter o processamento antes!

- Cria uma view vazia, chamada "_AvisoGeral".

- Forma atual de chamar um "Parcial view"

<blockquete> < partial name="_AvisoGeral" /> </blockquete>

- Chamada asyncrona

<blockquete> @await Html.PartialAsync("_AvisoGeral") </blockquete>

- Com o bantMark, deve se ver de qual forma renderiza mais rapido.

- Para que serve: Reaproveitar itens de formulario.

# Meu primeiro View Component

 - Criando um View Component

    - Cria a pasta "ViewComponents" para guardar os viewComponent.

    - Cria um classe chamado "CarrinhoViewComponent".

    - herda a classe "ViewComponent".

    - Cria uma propriedade chamada "itensCarrinho", dentro do construtor recebe os dados do banco e bota na variavel.

    - O método é obrigatorio se chama "InvokeAsync".

    - Bote um decoreito com um nome "[ViewComponent(Name = "Carrinho")]".

    <blockquete>
        [ViewComponent(Name = "Carrinho")]
        public class CarrinhoViewComponent : ViewComponent
        {
            public int itensCarrinho { get; set; }

            public CarrinhoViewComponent()
            {
                itensCarrinho = 3;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                return View(itensCarrinho);
            }
        } 
    </blockquete>

 - Criando uma View para o ViewComponent

 - Para que serve:  Criar componentes que trabalham como pequenos pedaços de uma View, porém de forma independente de um modelo específico e que podem realizar processamento, até mesmo consultar dados.

    - Na pasta Shared cria uma pasta chamada "Components", que guarda as view do component.

    - Cria uma pasta "Carrinho" que é o mesmo nome da ViewComponent.

    - Dentro da pasta cria um arquivo chamado "Default.cshtml" que faz o papel da view do component.

    - "@model" com "m" minusculo define.

    - "@Model" com "M" está exibindo o valor.

    <blockquete>

        @model int

        <span class="fade fa-shopping-cart fa-2x"> @Model </span>

    </blockquete>

 - Como chamar o seu viewComponent

    - Usando uma tagHelp chamada "vc"

    <blockquete>
      <vc:Carrinho></vc:Carrinho>
    </blockquete>

    - Porm deve configurar essa taghelp no "ViewImport"

    <blockquete>
        @addTagHelper "*, MinhaDemoMvc"
    </blockquete>


 - View Components e Partial Views são recursos diferentes para atender necessidades diferentes.

 - Os View Components são recursos extras para resolver problemas onde as Views ou Partial Views possuem certas limitações.

# Trabalhando com formulários

 - Crie uma pasta chamada "Filmes" na pasta Views, depois cria uma arquivo chamado "Adicionar".

 - ViewData: é aonde fica dados temporarios na memoria, e o titulo é um index, aonde você pode resgatar os valores.

 <blockquete>
 @{ 
    ViewData["Title"] = "Adicionar Novo Filme";
 }
    
    <h1>@ViewData["Title"]</h1>
 </blockquete>

 - Para poder usar o valor deve-se por @, para ele não interpretar como texto e sim como valor.

 - Cria o primeiro formulario.

 <blockquete>

    <div class="row">
        <div class="col-md-4">
            <form asp-action="Adicionar">

                <div class="form-group">
                    <label asp-for="Titulo" class="control-label"></label>
                    <input asp-for="Titulo" class="form-control" />
                </div>         

            </form>
        </div>
    </div>
 </blockquete>

 - Cria um controller com nome de "Filmes"

 <blockquete>

    public class Filmes : Controller
    {
        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }
    }
 </blockquete>

 - Na view Layout, bota uma referencia da controller Filmes

 <blockquete>
    < li class="nav-item">
        < a class="nav-link text-dark" asp-area="" asp-controller="Filmes" asp-action="Adicionar">Filmes</>
    < /li>
 </blockquete>


# Validações de Formulário

 - Reaproveitando a validação da model.

 - Em cada campo adiciona um span com o codigo asp fazendo referencia ao atributo da model

 <blockquete>
  < span asp-validation-for="Titulo" class="text-danger"></>
 </blockquete>

 - Para mostrar um resumo das validações, se usa um validacionSumer

    - ModelOnly: apenas na modelState.

    - All: tanto da modelState , tambem validações antes de subimeter o formulario.

 - É preciso revalidar a modelState, fazendo uma validação manual.

    - Validação antes se submeter para controller, precisa asicionar uma section.

    - Com isso carrega toda regra para validar, usando o jQuery, a mensagem fica em portugues !

    <blockquete>
                    
        @section Script{ 

            @{
                await Html.RenderPartialAsync("_ValidationScriptsParcial");
             }
        }

    </blockquete>


# Ganhe tempo. Utilize Scaffold

    - cria automaticamente formularios, view e controller

    - add -> controller

        - MVC + antityframework

        - escolhe a model já existente

        - escolha o contexto

        - marca todo os campos

        - ele já cria o nome do controller!

    - Criando uma partialView

        - add view -> partialView/view
 

# Criando um projeto MVC sem template

    - Cria uma solução em branco

    - Cria uma pasta chamada "src", "test", "tools", "docs" dentro do projeto.
    
    - Cria um projeto "ASP.NET Core Web Application" VAZIO!

    - Cria as pastas "Models", "Views" e "Controller".

    - Configurando o MVC no arquivo Startup.
            
        <blockquete>

                public void ConfigureServices(IServiceCollection services)
                {
                    services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
                }
        </blockquete>

            - Com isso fica compativel com uma versão especifica do dotnet ou com a ultimaversão

    - Configurando rotas caso seja usada!

        - trocando o  app.run, pelo o app.MVC, no arquivo StartUp(vai da erro, modo antigo)

        - Caso seja necessario configurar rotas.

        - Link para ajudar a configurar de forma nova.

        https://stackoverflow.com/questions/57684093/using-usemvc-to-configure-mvc-is-not-supported-while-using-endpoint-routing



 <blockquete>
            // MODELO NOVO
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            // MODELO ANTIGO VAI DA ERRO!
            // app.UseMvc(routes =>
            // {
            //     // routes.MapRoute(
            //     //     name: "default",
            //     //     template: "{controller=Home}/{action=Index}/{id?}");

            //     routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
                
            // });

            // app.UseMvc();

  
 </blockquete>
 
    - Cria uma controller do zero na pasta controllers

    - cria uma view na pasta com nome da controller dentro da view.
    
 # Ferramentas de Front-End

    - Cria uma pasta chamada Shared, depois uma view chamada "_Layout".

    - Chama o "@RenderBody()" na tag <main> da view "_Layout", ele é responsavel pelo conteudo.

    - Cria um arquivo chamado "_ViewStart" e define o layoutu padrão nela que vai ser uma outra view chamada "_Layout".

 <blockquete>
        @{
        Layout = "_Layout";
        }
 </blockquete>

### Fazendo o TagHelpers funcionar.

    - Cria o arquivo "_ViewImport" dolado da "_ViewSatrt", e adiciona o codigo.

 <blockquete>

    @using Dev.UI.Site
    @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers

 </blockquete>

    - Se cria esse codigo para os TagHelpers poder ser reconhecido.

### implementando o bootstrap

    - Cria uma pasta chamada "wwwroot", adiciona na aplicação usado a opção "Biblioteca ao lado do cliente".

    - Com a opção cdnJS, escolhe as libs.

    - Depois bota a referencia dos arquivos no view "_Layoutcshtml"

### decidindo se o arquivo carrega

 - A tAG "environment" define oque vai carregar em produção ou não.
 
 <blockquete>

    < environment include="Development">
        <link rel="stylesheet" href="~/lib/bootstrap/css/bootstrap.css" />
        <link rel="stylesheet" href="~/lib/font-awesome/css/fontawesome.css" />
    < /environment>
    < environment exclude="Development">
        <link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/css/bootstrap.min.css"
              asp-fallback-href="~/lib/bootstrap/css/bootstrap.min.css"
              asp-fallback-test-class="sr-only" asp-fallback-test-property="position" asp-fallback-test-value="absolute"
              crossorigin="anonymous"
              integrity="sha256-eSi1q2PG6J7yAaWMcrr5GrtohYChqibrV7PBE=" />
    < /environment> 

 </blockquete>
 
 - asp-fallback-href: caso não carrega o cdn ele vai carregar o arquivo indicado pelo " asp-fallback-href".

 - integrity: codigo que autentica o arquivo cdn.

 - crossorigin: fala que é o mesmo arquivo.

 - "@ViewData["Title"]" é o mesmo que "@ViewBag["Title"]"


# Bundling & Minification

 - cria um arquivo e pasta css e js, dentro da pasta wwwroot, para personalização propria;

 - Cria um arquivo "bundlingConfig" no projeto.
 
 <blockquete>
    {
    "outputFileName": "wwwroot/css/site_bundle.min.css",
    "inputFile": [
        "wwwroot/lib/bootstrap/css/bootstrap.css",
        "wwwroot/css/site.css"
    ]

    },
    {
    "outputFileName": "wwwroot/js/site_bundle.min.js",
    "inputFile": [
        "wwwroot/lib/jquery/jquery.js",
        "wwwroot/js/site.js"
    ]

    }
 </blockquete>

 - Procure a aba de task runner explorer()

 <blockquete>
 </blockquete>

# Custom Tag Helpers

 - Cria uma pasta chamada "Extensions" e um arquivo chamado "EmailTagHelper".

 - 

 <blockquete>

    public class EmailTagHelper: TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //return base.ProcessAsync(context, output);
            // Define uma saia, define o "a" como tagHelp que é a saida.
            output.TagName = "a";

            // Busca o conteudo da tag
            var content = await output.GetChildContentAsync();

            // Gerando uma saida
            var target = content.GetContent() + "@" + "desenvolvedor.io";

            // setando os valores
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);

        }
    }

 </blockquete>
    
    ### Registrando TagHelper

    - No arquivo _ViewImports, e bota o codigo que reconhece todas as taghelper.

 <blockquete> @addTagHelper "*, Dev.UI.Site" </blockquete>

    ### Recebendo valores no taghelper

    - Cria uma propriedade publica.

<blockquete> public string EmailDomain { get; set; } = "desenvolvedor.io"; </blockquete>

    - Chamando a taghelper

<blockquete>
    <footer class="border-top footer text-muted">
            <div class="container">
                Minha App Modelo - <email email-domain="gmail.com">contato</email> -
                <email>contato</email>
            </div>        
        </footer>
</blockquete>

# Como funcionam as Areas?

 - As áreas proporcionam uma maneira de organizar uma aplicação ASP.NET MVC em grupos funcionais menore, cada um com seu próprio conjunto de Models, Views e Controllers.

 - Uma área é efetivamente uma nova estrutura MVC dentro da aplicação ASP.NET MVC.

 - Os components lógicos como Models, Views e Controllers são mantidos em pastas diferenes. Podendo utilizar recursos diferetes ou compartilhados da aplicação.

 - Para criar um novo, vai no projeto -> add -> "novo item com scanffold"

 - Cria uma controller e uma view.
 
 - No layout define a area com "asp-area", "asp-controller" e o "asp-action" 

 <blockquete>
   < li class="nav-item">
        < a class="nav-link text-dark" asp-area="Produtos" asp-controller="Cadastro" asp-action="Index">Cadastro</>
    < /li>
 </blockquete>
 
 - No controller deve por o decoraito "area" 

 <blockquete> [Area(nameof(Produtos))] </blockquete>

 - Por ultimo define a area na rota pelo arquivo startUP

 <blockquete>
    endpoints.MapAreaControllerRoute(
                   "teste",
                   "Produtos",
                   "areas/{controller=Cadastro}/{action=Index}/{id?}");
 </blockquete>

 - pode por "exist" dolado de areas para verificar se ela exist!

# Exemplos de utilização
 
 - Pode por na pasta view da area, uma copia do arquivo "_viewStart" para exibir o manu.
 - Caso queira trocar o nome da pasta "area" deve fazer uma configuração dentro do metodo "ConfigureService" do arquivo StartUp.

    <blockquete>

    services.Configure<RazorViewEngineOptions>(optins => {
                    optins.AreaViewLocationFormats.Clear();
                    optins.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                    optins.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                    optins.AreaViewLocationFormats.Add("/View/Shared/{0}.cshtml");
                });

    </blockquete>
 ### Oque motiva a ter uma área

 - Configurar rotas no arquivo startUp é opcional.

 - Pode congiruar as rotas das areas no proprio controler.

    <blockquete>

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
    </blockquete>

# Injeção de dependência (DI)
 
 - É um pa drão de design de codificação que faz parte dos principios SOLID.

 - A ideia é obter a Ioc(inversão de controle) para simplificar as responsabilidades de uma classe.

 - O ASP.NET Core dá suporte a injeção de dependência de forma nativa, porém é possível trabalhar com outros conteiners como SimpleInjector, Autofac e etc.

# Configurando uma injeção de dependência

 - Cria uma classe "Pedido" com a propriedade id, do tipo guid.

 - Uma interface com método que retorna um valor de "Pedido".

 - E Uma classe que implementa esse método.

 - Com isso deve se configurar uma Injeção de dependencia no método "ConfigureService", no arquivo startUp. 

 - Com o método "AddTransient" ele cria automaticamente uma instancia da classe, não deixa de criar, implementar e por a Interface.

 <blockquete>

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IPedidoRepository, PedidoRepository>();            
    }

 </blockquete>

 # Injetando dependências no MVC

 - Ultilizando a Injeção de dependencia no controller.

 <blockquete>

    private readonly IPedidoRepository _pedidoRepository;

        public HomeController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Index()
        {
            var pedido = _pedidoRepository.ObterPedido();

            return View();
        }
 </blockquete>
 
 - Ultilizando Injeção de dependencia na View.

 <blockquete>
    @using Dev.UI.Site.Data
    @inject IPedidoRepository PedidoRepository
    Ola! @PedidoRepository.ObterPedido().Id; 
 </blockquete>

 ### sistema legado que não pode alterar o construtor.

 - é pissivel injetar a dependendica sem ser no construtor, injetando diretamente no método!

 <blockquete>
    public IActionResult Index([FromServices] IPedidoRepository _pedidoRepository)
    {
        var pedido = _pedidoRepository.ObterPedido();

        return View();
    }
 </blockquete>

# Tipos de Ciclo de Vida

 - Transiente: Obtém uma nova instância(uma nova locação de memoria) do objeto a cada solicitação

 - Scoped: Reutiliza a mesma instância do objeto durante todo request(web).

 - Sigleton: Ultiliza a mesma instantância para toda aplicação(cuidado), nunca muda mesmo com novos request.

# Entity Framework Core

 - O Entity Framework Core é o componente ideal para trabalhar com o 
 novo stack do ASP.NET Core.

 - Está pronto para utilização, porém ainda possui um roadmap de melhorias
 e novas funcionalidades que deverão ser entregues.

 - É possível trabalhar com EF 6.x no ASP.NET Core, mas será necessário escrever o códogo usando
 o .NET Full (4.x), o que não é recomendado uma vez que o ASP.NET Core 3
 não erá trabalhar com .NET Full (4.x).

# Instalando o EF Core

 - Vai nos pacotes Nugets, pesquisa por "Microsoft.EntityframeworkCore"!

 - Instala a ultima verção estavel.

 - Inatalação manual:
 
 <blockquete> Install -pacjge Microsoft.EntityFrameworkCore </blockquete>
 <blockquete> Microsoft.EntityFrameworkCore.SqlServer </blockquete>
 <blockquete> Microsoft.EntityFrameworkCore.SqlServer.Design </blockquete>
 <blockquete> Microsoft.EntityFrameworkCore.Tools </blockquete>

# Configurando o DbContext

 - Cria uma classe chamado "meuDbContext", você vai herdar a classe "dbContext".

 - Ela se torna uma classe de contexto, aonde mapeia as classes, ligando ao
 banco de dados.

 ### Arquivo appSettings.json

 - No arquivo "appSettings.json" você informa a sua connectionStrings(chave).

 - Como o valor bota um onjeto que tem a chave "MeuDbContext", e valor "servidor\\instanciaDoservidor;BaseDeDados;informaçõesAmais"

 <blockquete> 
  "ConnectionStrings": {
    "MeuDbContext":  "Server=(localdb)\\mssqllocaldb;Database=MeuBancoDoCursoAspNet;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
 </blockquete>

 - Trusted_Connection: usuaria confiavel dentro do sql.

 - MultipleActiveResultSets: Aceita multiplos varios resultados simuntaneamente.

 ### Arquivo StartUp

 - Deve configurar a classe dbContext, dentro da classe startUp!

 - Aonde você informa o contexto, e qual banco ele vai conectar(connectionStrings)

 - Cria uma propriedade chamada "Configuration" do tipo "IConfiguration"
 ela recebe um valor pelo construtor.
 
 <blockquete>

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

 </blockquete>

 - Busca um atalho para sua ConnectionString

 <blockquete> 
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MeuDbContext>(optionsAction:options => 
                options.UseSqlServer(Configuration.GetConnectionString(name:"MeuDbContext")));
    }   
 </blockquete>

 ### Arquivo meuDbContext

 - Cria um método construtor recebendo o options, configuração basica do contexto. 

 <blockquete>
    public class MeuDbContext: DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options)
        :base(options)
        { //TODO }
    }
 </blockquete>

# Operações CRUD

 - Cria uma classe chamada "Aluno", com propriedade Id do tipo Guid.

 - Cria uma controller vazia chamada "TesteCrudController"

 - No controller faz a injeção de dependencia do contexto para fazer o CRUD

 - Cria valores pre definidos apenas para fazer teste no mesmo método.

 <blockquete>

    private readonly MeuDbContext _contexto;

    public TesteCrudController(MeuDbContext contexto)
    {
        _contexto = contexto;
    }

    public IActionResult Index()
    {
        // Adicionando Alunos
        var aluno = new Aluno
        {
            Nome = "Lincoln",
            DataNascimento = DateTime.Now,
            Email = "link@email.com.br"
        };
        _contexto.Alunos.Add(aluno);
        _contexto.SaveChanges();

        // Consultando Aluno por id ou email, busca apenas 1.
        var aluno2 = _contexto.Alunos.Find(aluno.Id);
        var aluno3 = _contexto.Alunos.FirstOrDefault(a => a.Email == "link@email.com.br");
        
        // Buscando uma coleção de aluno, busca quantos existir.
        var aluno4 = _contexto.Alunos.Where(a => a.Nome == "Eduardo");

        // Editando Aluno
        aluno.Nome = "João";
        _contexto.Alunos.Update(aluno);
        _contexto.SaveChanges();

        // Removendo Aluno
        _contexto.Alunos.Remove(aluno);
        _contexto.SaveChanges();

        return View();
    }

 </blockquete>

 ### Botando a classe no Arquivo de contexto

 - Cria um propriedade chamada "Alunos"
 
 <blockquete>  public DbSet<Aluno> Alunos { get; set; } </blockquete>


# Trabalhando com Migrations

 - O Migrations ele olha para o banco verifica se existe, para poder criar
 um script que cria todas as tebelas com as colunas.

 - Primeiro devese criar uma Migration com o comando no diretorio do projeto:
 
 <blockquete> dotnet ef migrations add "nomeDpMigration" -Context "nome do context" </blockquete>

 - Só bota o nome do contexto caso tenha mais que 1 contexto

 ### Removendo coluna

 - Caso queira remover digita o comando.

 - Se remove, alterando o o bjeto aluno e fazendo uma nova migração e update

 <blockquete> ef migrations remove </blockquete>

 - Atualiza o banco de dados, para executar o codigo gerado
 
 <blockquete> dotnet ef database update </blockquete>

 ### update nova propriedade.

 - Para adicionar uma nova propriedade deve, modificar a classe, e repetir os mesmos comandos.
 - Use um nome diferente na migration.

 <blockquete> dotnet ef migrations add "nomeDpMigration" -Context "nome do context" </blockquete> 
 <blockquete> dotnet ef database update </blockquete>

 ### Deletando o banco

 - Pode esta deletando o banco de dados e fazendo o update, que o migration vai recriar tudo de novo só que sem os dados.

 ### Testando o CRUD

 - CRUD está funcionando.


 <blockquete>
 </blockquete>



 - 

 <blockquete>
 </blockquete>


 - 

 <blockquete>
 </blockquete>


 - 

 <blockquete>
 </blockquete>




