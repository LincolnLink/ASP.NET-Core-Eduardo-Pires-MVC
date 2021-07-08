# ASP.NET-Core-Eduardo-Pires-MVC
Aulas de MVC com Eduardo Pires, asp.net CORE


# O padrão MVC

- Ele é um padrão arquitetural

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

# Trabalhando com Action Results

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

- _ViewStart: informa qual pagina sera a principal da aplicação.

- _ViewImports: importa component, que será usado de forma global.

- _Layout: pagina mestre.

# Apresentando as Partial Views

- Partial Views são pedaçoes de uma view que possuem dados e que podem ser reaproveitados em N views, assim proporcionando mais reaproveitamento de código.

- As partial views dependem do modelo implementado na view principal, gerando certa limitação no seu uso.

As partial view são muito ultilizadas também para rederizar dinamicamente parte de uma view através de requisições AJAX.

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

<blockquete> 

< partial name="_AvisoGeral" />

</blockquete>

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
 
 <blockquete>  

    public DbSet<Aluno> Alunos { get; set; } 

 </blockquete>


# Trabalhando com Migrations

 - O Migrations ele olha para o banco verifica se existe, para poder criar
 um script que cria todas as tebelas com as colunas.

 - Primeiro devese criar uma Migration com o comando no diretorio do projeto:
 
 <blockquete> 

    dotnet ef migrations add "nomeDpMigration" -Context "nome do context" 

 </blockquete>

 - Só bota o nome do contexto caso tenha mais que 1 contexto

 ### Removendo coluna

 - Caso queira remover digita o comando.

 - Se remove, alterando o o bjeto aluno e fazendo uma nova migração e update

 <blockquete> 

    ef migrations remove 

 </blockquete>

 - Atualiza o banco de dados, para executar o codigo gerado
 
 <blockquete> 

    dotnet ef database update 

 </blockquete>

 ### update nova propriedade.

 - Para adicionar uma nova propriedade deve, modificar a classe, e repetir os mesmos comandos.
 - Use um nome diferente na migration.

 <blockquete> 

    dotnet ef migrations add "nomeDpMigration" -Context "nome do context" 

 </blockquete> 

 <blockquete> 

    dotnet ef database update 

 </blockquete>

 ### Deletando o banco

 - Pode esta deletando o banco de dados e fazendo o update, que o migration vai recriar tudo de novo só que sem os dados.

 ### Testando o CRUD

 - CRUD está funcionando.

# Segurança com ASP.NET Identity

 - O ASP.NET Identity é um componente de segurança que trabalha com Autenticação e
 Autorização de usuário e possui diversas funcionalidades.

 - O Identity está na versão 3.0(versão Core) e é basicamente uma portabilidade da versão 2.0(.NET Full 4.x) para a nova plataforma.

 - Algumas mudanças ocorreram na versão 3.0 o que o torna muito mais fácil de implementar e abstrair.

 ### Camadas Identity no MVC.

 - Data Access Layer: Conecta com o banco de dados. (Camada de dados)

 - Identity Store: faz as percistencia no banco! (Camada de dados)

   - Exemplo: UserStore, RoleStore.
 
 - Identity Manager: A plicação ASP.NET Core App ela interage com essa camada (Camada de negocios)

   - Exemplo: UserManager, RoleManager.

# Configuração

 - Cria uma solução nova, um projeto mvc novo.
 
 - Pode está instalando o Identity na hora de criar o projeto de forma automatica, não escolha pois o tutotias explica de forma manual.

 - Instala o pacote de suporte:
 
 <blockquete>

    Install-Package Microsoft.AspNetCore.Identity.UI

 </blockquete>
 
 - O Identity pode por em uma area.

 - Usando o Scaffolding, escolha a opção "Identity" extraia um "login" e um "registro", cria um contexto junto.

 - Usa a mesma versão do projeto, para evitar erros!

 - O Scaffolding gera as pages, um novo startup e um contexto para gerar tabelas

 - O Contexto dele é baseado em "IdentityDbContext", que gera de dbConext.

 - É bom deixar o context do Identity separado.

 ### Configuração no StratUp

 - No arquivo "IdentityHostingStartup" remove a configuração de "service" e bota no arquivo de startUp original.


 - ele gera um connectString!
 
 <blockquete>
    
    "ConnectionStrings": {
        "AspNetCoreIdentityContextConnection": "Server=(localdb)\\mssqllocaldb;Database=AspNetCoreIdentity;Trusted_Connection=True;MultipleActiveResultSets=true"
    }

 </blockquete>

 - IdentityUser é uma classe que trabalha como se fosse um usuario na aplicação.

 <blockquete>

    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddDefaultUI()
        .AddEntityFrameworkStores<AspNetCoreIdentityContext>();
 
 </blockquete>

 - AddfaultUI: configuracao das pages, o Padrão é BootStrap4.

 - AddEntityFrameworkStores: está configurando o tipo de acesso a dados que o Identity vai usar.

 - Ele cria um segundo startUp, apaga e bota a configuração na principal.

 - Link util: https://github.com/aspnet/Announcements/issues/380

 - https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio

 <blockquete>

    services.AddDbContext<AspNetCoreIdentityContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));

    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddDefaultUI()
        .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

 </blockquete>

 - No final do arquivo "stratUp" usa o método "app.UseAuthentication();" para o Identity funcionar.

 ### Configurando as tabelas
 
 - Executa os metodos para gerar as tabelas.

 <blockquete>

    dotnet ef migrations add "Identity"

 </blockquete>
 
 <blockquete> 

    dotnet ef database update 

 </blockquete>

 ### Page 

 - Toda Page herda da classe "PageModel"

    - OnGetAsync(): É um método que faz retornar uma view via get.

    - OnPostAsync(): Executado quando faz um Post atraver da view.

 - No Identity não tem view e sim page ( @page )

 - Implementa a ViewParcial que tem a tela de login.

 - Na tela "_Layout.cshtml" bota a viewParcial "_LofinPartial.cshtml"

 <blockquete>

  < partial name="_LoginPartial" />

 </blockquete>

 ### Rotas do Identity MVC

 - No startup adiciona "endpoints.MapRazorPages();", para as rotas funcionar.
 
 <blockquete>

    app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

 </blockquete>

 - Depois importa outra pagina do Identity chamada "TwoFactorAuthenticationModel" na pasta area.

 - Para editar qualquer pagina é preciso repetir o processo de improtação.

 - codigo da documentação para configurar dotnet 3.1.

 ### Codigo extra para funcionar! (rota)

 - link: https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio

 <blockquete>

    services.AddRazorPages();

    services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

    services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

 </blockquete>

# Autenticação ( [Authorize] )

 - É autenticação mas o atributo se chama [Authorize]!

 - O metadado "[Authorize]" cria um bloqueio na controller/view, caso o usuario não esteja logado, pode por na classe, bloqueando todas as paginas.

 - Pode por em toda a controller ou apenas no método da aquela pagina que você quer bloquear.
 
 <blockquete>

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

 </blockquete>

 - [AllowAnonymous] é um metadado que abre exceção, assim o usuario consegue ver uma pagina para logar.

 - O método "PasswordSignInAsync" que fica na Page de Login, faz a autenticação guardando os dados no cookie.

 - PasswordSignInAsync: Faz validação do usuario e senha, faz a autenticação e bota no cookie.

# Autorização ( Roles )

 - É um nivel a mais, alem de está logado deve ter a autorização/ poder, de está vendo a pagina.

 - Usando a palavra chave "Roles" no metadado [Authorize]!

 - No startup deve por a configuração do Role ".AddRoles<IdentityRole>()"

 <blockquete>

    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

 </blockquete>

 - Na tabela "dbo.AspNetRoles" você cria a configuração das "Roles", botando id, Name, NormalizedName, Concurre...

 - Na Tabela "dbo.AspNetUserRoles" você cria um vinculo entre o id do seu usuario, com o id da roule existente que você deseja vincular.

 - Para definir essa configuração, deve implementar a roule no método de controller que você deseja bloquear.

<blockquete>

    [Authorize(Roles = "Admin")]
    public IActionResult Secret()
    {
        return View();
    }

</blockquete>

- Pode definir duas Roles uma dolado da outra 

<blockquete>

    [Authorize(Roles = "Admin, Gestor")]
    public IActionResult Secret()
    {
        return View();
    }

</blockquete>

 - Da trabalho em trocar todos os locais que tem aquela "Roler".

 - 


# Trabalhando com Claims

 - Claims: pode ser qualquer coisa, exemplo : usuario, email, aonde estudou, etc, armazena qualquer tipo de informação.

 - As Claims são guardadas no Cookies!

 - A tabela "dbo.AspNetUserClaims" é a que vincula as Claims com o usuario.

 - Alem da configuração no controller deve configurar no startup
 
 - Policy: uma configuração das Claims, que deve ser definida no arquivo de StartUp e no arquivo de Controllers!

 - No Controllers: É parecido com as Roles

 <blockquete>

    [Authorize(Policy = "PodeExcluir")]
    public IActionResult SecretClaim()
    {
        return View("Secret");
    }

 </blockquete>

 - Na StartUp: 

 <blockquete>

    services.AddAuthorization(options =>
    {
         options.AddPolicy(
            name:"PodeExcluir",
            configurePolicy: policy => policy.RequireClaim("PodeExcluir")
        );
    });

</blockquete>

 - Deve ter uma Claim para cada permissão.

 ### Roles + Claims

 - Pode vincular Roles com Claims , cadastrando o vinculo na tabela, "dbo.AspNetRoleClaims"!

 - Associando o id da Role com o tipo de Claim!

 ### Autorização personalizada (Recomendada pela Microsoft)

 - Toda "policy.Requirements" do arquivo StartUp deve ter uma "AuthorizationHandler".
 
 - Chama o método na StartUp, para validar a autorização.

 <blockquete>

  services.AddAuthorization(options =>
    {
        options.AddPolicy(
            name: "PodeLer", 
            configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer"))
        );

        options.AddPolicy(
            name: "PodeEscrever", 
            configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever"))
        );
    }

 </blockquete>

 - Cria uma pasta chamada "Extensions" depois uma arquivo chamada "AuthorizationHelper.cs"

 - Dentro cria uma classe chamada "PermissaoNecessaria" que implementa a interface "IAuthorizationRequirement".

 - Essa classe serve apenas para pegar um valor string pelo construtor.

<blockquete>

    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; }

        public PermissaoNecessaria(string permissao)
        {
            Permissao = permissao;
        }
    }

</blockquete>

- Cria uma segunda classe, chamado "PermissaoNecessariaHandler", que implementa AuthorizationHandler<T>.

- O no lugar do "T" coloca a primeira classe!

- faz um Override do método HandleRequirementAsync().

- Verifica se o tipo é igual "Permissao", e se tem a claim passada por parametro.

- Verifica se o usuario tem o tipo da Claim e o valor da Claim, definido na tabela "dbo.AspNetUserClaims".

 <blockquete>

    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requisito)
        {            
        if(context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requisito.Permissao)))
        {
            // Informa que foi sucesso!
            context.Succeed(requisito);
        }

        // Termina a Task
        return Task.CompletedTask;
        }
    }

 </blockquete>

 - No Controller cria um metodo novo e uma page nova com o nome de "SecretClaimEscrever",
  passando a claim "SecretClaimEscrever". 

 <blockquete>

    [Authorize(Policy = "PodeEscrever")]
    public IActionResult SecretClaimEscrever()
    {
        return View();
    }

 </blockquete>

 - Por final na classe StartUp cria uma injeção de dependencia.

 <blockquete>

    services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

 </blockquete>

# Atributo personalizado, Customizando a autenticação da App (Recomendade pelo Eduardo Pires)

 - Cria uma classe chamada "CustomAuthorization" na pasta "Extensions"
 
 - Cria 3 classes nesse arquivo, a ideia é criar um atributo para validar na controller.

 - 1° Classe deve ter a logica de verificar as claims e ver se está autenticado.

 <blockquete>

        public class CustomAuthorization
        {       
            public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
            {
                return context.User.Identity.IsAuthenticated &&
                    context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
            }
        }   

 </blockquete>


 - 2° class recebe o valor, é um filtro do ASP.NET,
  definindo um filtro usando a interface "IAuthorizationFilter".

 <blockquete>

        public class RequisitoClaimFilter : IAuthorizationFilter
        {
            private readonly Claim _claim;

            public RequisitoClaimFilter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {

                //Verificando se está autenticado, e redireciona para pagina de login.
                if(!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new {
                        area = "Identity",
                        page = "/Account/Login",
                        ReturnUrl = context.HttpContext.Request.Path.ToString()
                    }));

                    return;
                }

                // Método static pode usar diretamente!
                if(!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
                {
                    // Se ele não passar na validação vai da acesso negado.
                    context.Result = new ForbidResult();
                }
            }
        }

 </blockquete>

 - 3° class tem o objetivo de transformar a 2° classe em atributo, com a interface "TypeFilterAttribute"

 - Converte a classe filtro em atributo para usar na controller.

 <blockquete>

        public class ClaimsAuthorizeAttribute : TypeFilterAttribute
        {
            public ClaimsAuthorizeAttribute(string claimName, string claimValue) 
            : base(typeof(RequisitoClaimFilter))
            {
                Arguments = new object[] { new Claim(claimName, claimValue) };
            }
        }

 </blockquete>

 ### Ultilizando o atributo personalizado.
  
 - Cria um novo metodo e view chamado "ClaimsCustom"

 - Essa validação é feita de uma forma diferente.

 <blockquete>

        [ClaimsAuthorize("Home","Secret")]
        public IActionResult ClaimsCustom()
        {
            return View();
        }

 </blockquete>

# Logica dos atributos usando nas Razo Page.
 
 - Cria uma classe chamada "RazorExtensions", com os 3 métodos, 
 
  - 1° Se quizer validar algo na view razor.
  - 2° Caso queira desabilitar um botão.
  - 3° Caso queira desabilidatr um link.

 <blockquete>
  
        public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);
        }

        public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue) ? "" : "disabled";
        }

        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(context, claimName, claimValue) ? page : null;
        }

 </blockquete>

 ### Ultilizando "Logica dos atributos usando nas Razo Page"
 
 - Em uma view qualquer, bota a logia:

 <blockquete>
 
    @using AspNetCoreIdentity.Extensions

    @{
        if (this.IfClaim("Produtos", "Adicionar"))
        {

            <p> Você só verá isso se tiver permissão de adicionar!</p>
        }
    }

        
    <div>
        @Html.ActionLink("Secret", "Secret").IfClaimShow(Context ,"Produtos", "Adicionar")
    </div>
    <br />
    <div href="#" class="btn btn-danger @Html.Raw(this.IfClaimShow("Produtos", "Excluir"))">
        Exluir
    </div> 

 </blockquete>
 
# Trabalhando na classe Startup.cs

 - Cria um método de extenção, para deixar a classe StartUp mais limpa.

 - Cria uma class static chamada "DependencyInjectionConfig" com o método static "ResolveDependencies".

 - Deve retornar "IServiceCollection".

 - Métodos de extenção deve ser estatic e criado em classes static.

 - O método recebe como parametro "this IServiceCollection services"

 - e retorna um " return services; "

 - No corpo do metodo deve ter todas as configurações de Injeção de dependencia, que ficava no arquivo StartUp

 <blockquete>

    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Regra vale para todos, 
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            return services;
        }        
    }

 </blockquete>
 
 - o Método é chamado na startUp

 <blockquete>

    // As Injeções de Dependencia
    services.ResolveDependencies();

 </blockquete>

 - Esse mesmo procedimento foi feito com o método "AddIdentityConfig" que recebeu as configurações do Identity,
 que está na classe "DependencyInjectionConfig".

 - Esse mesmo procedimento também foi feito com o método "AddAuthorizationConfig" que ficou na classe "IdentityConfig",
 esse método recebe toda a configuração das CLAIMs.

# Ambientes de execução

 - Estudando o arquivo "launchSettings", aonde é configurado o ambiente.

 - Define 4 ambientes diferentes nos Prodiles!       

 <blockquete>

        "profiles": {

            "IIS - Dev": {
                "commandName": "IISExpress",
                "launchBrowser": true,
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Development"
                }
            },

            "IIS - Staging": {
                "commandName": "IISExpress",
                "launchBrowser": true,
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Staging"
                }
            },

            "IIS - Prod": {
                "commandName": "IISExpress",
                "launchBrowser": true,
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Production"
                }
            },

            "Self Hosting": {
                "commandName": "Project",
                "launchBrowser": true,
                "applicationUrl": "http://localhost:5000",
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Development"
                }
            }
        }

 </blockquete>
 
 - Cria o arquivo "appsettings.Development.json", "appsettings.Production.json", "appsettings.Staging.json", apenas duplicando.

 - Todos eles recebe a connectionString atual(a do localhost), Remove a connectionString do "appsettings.json"

 ### Vinculando os ambientes com cada appsettings correspondente.

 - Esse vinculo é feito na classe StartUp

 <blockquete>

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile(path:"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }


            Configuration = builder.Build();
        }

 </blockquete>

# Protegendo dados com User Secrets.

 - Informa pro sistema que quando o ambiente for de produção, 
 vai pegar a conectionsString de um outro lugar.

 - Essa condição é colocada no StratUp.

 <blockquete>

          if (hostEnvironment.IsProduction())
          {
            builder.AddUserSecrets<Startup>();
          }

 </blockquete>

 - Para criar um "User Secret", deve apertar com o botão direito do mouse no projeto, 
 e escolher "Manager User secret" ou "Gerenciar Segredos do Usuario"

 - Esse usuario é o mesmo usuario do windows da sua maquina, no caso só funciona na sua maquina.

 - No arquivo copia e cola a sua conecctionsString!

<blockquete>

        "ConnectionStrings": {
        "AspNetCoreIdentityContextConnection": "Server=(localdb)\\mssqllocaldb;Database=AspNetCoreIdentity;Trusted_Connection=True;MultipleActiveResultSets=true"
        }

</blockquete>

# Tratamento de erros.

 - Cria um erro proposital para conhecer a diferença da tela, de erro em produção para erro em desenvolvimento.

 - Na StartUp existe um tratamento de erro padrão.

 - app.UseDeveloperExceptionPage(): retorna informações do erro, caso tenha algum, 
 Caso o ambiente seja de desenvolvimento. 

 - Se não, encaminha para uma view de erro!

<blockquete>

        if (env.IsDevelopment())
        {                
            app.UseDeveloperExceptionPage();
        }
        else
        {                
            app.UseExceptionHandler("/Home/Error");
        }

</blockquete>

 - No arquivo "ErrorViewModel" subistitua as propriedades.

<blockquete>

        public int ErroCode { get; set; }

        public string Titulo { get; set; }

        public string Mensagem { get; set; }

</blockquete>

 - Na view "Error.cshtml" deve trocar a mensagem de erro.
  
<blockquete>
     
        @{ 
            if(Model == null)
            {
                <div>
                    <h2>Ooops! Ocorreu um erro, mas não se preocupe, 
                    Nosso time será avisado e iremos corrigir em breve</h2>
                </div>
            }
            else
            {
                <h1>@Html.Raw(Model.Titulo)</h1>
                <h2 class="text-danger">@Html.Raw(Model.Mensagem)</h2>
            }
        }
 
</blockquete>

 - Modifica a controller que faz vinculo com a tela de erro!

 - E cria uma tratamento para cada tipo de erro.

<blockquete>

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

</blockquete>

 - No StartUp, modifica o tratamento.

<blockquete>

        if (env.IsDevelopment())
        {
            // retorna informações do erro, caso tenha algum.
            // Caso o ambiente seja de desenvolvimento.
            app.UseDeveloperExceptionPage();
        }
        else
        {
            /* Se não, encaminha para uma view de erro!*/
            /* app.UseExceptionHandler("/Home/Error");*/
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();                
        }

</blockquete>

# Realizando o Log de tudo.

- Cria uma controller chamada "Teste".

- Injeta a interface "ILogger" no construtor,  

- No arquivo "AppSettings.json" você configura o Logger.

- Chama o método que declara um erro para o loger para testar. 

- No console ou no output, informa esse log.

<blockquete>

            public IActionResult Index()
            {
                _logger.LogError("Esse erro Aconteceu!");

                return View();
            }

</blockquete>

- É nessesario uma ferramenta de 3° para realizar o armazenamento dos log .

- KissLog.net é a ferramenta recomendada para fazer isso.

- No arquivo de "DependencyInjectionConfig" que fica na pasta "Config", deve por a injeção de dependencia do kisslog
 
<blockquete>

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });
            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

</blockquete>

 - No StartUp bota a confgiração do kisslog 

<blockquete>

        app.UseKissLogMiddleware(options => {
                ConfigureKissLog(options);
            });

</blockquete>

 - Cria um arquivo chamado "LogConfig" na pasta "Config".

 - Para isolar o condigo de configuração do log.

 - Esse codigo foi passado pela documentação do KissLog.

 - https://kisslog.net/Account/ApplicationConfiguration?applicationId=7a7704e7-1a95-42dc-ba20-62249b91a261

 <blockquete>

        public class LogConfig
        {
            public static void ConfigureKissLog(IOptionsBuilder options, IConfiguration Configuration)
            {
                // optional KissLog configuration
                options.Options
                    .AppendExceptionDetails((Exception ex) =>
                    {
                        StringBuilder sb = new StringBuilder();

                        if (ex is System.NullReferenceException nullRefException)
                        {
                            sb.AppendLine("Important: check for null references");
                        }

                        return sb.ToString();
                    });

                // KissLog internal logs
                options.InternalLog = (message) =>
                {
                    Debug.WriteLine(message);
                };

                RegisterKissLogListeners(options, Configuration);
            }


            public static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration Configuration)
            {
                // multiple listeners can be registered using options.Listeners.Add() method
 
                // register KissLog.net cloud listener
                options.Listeners.Add(new RequestLogsApiListener(new Application(
                    Configuration["KissLog.OrganizationId"],    //  "16bf8aa9-d986-44fc-815a-4a82a8012261"
                    Configuration["KissLog.ApplicationId"])     //  "7a7704e7-1a95-42dc-ba20-62249b91a261"
                )
                {
                    ApiUrl = Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
                });
            }
        }

 </blockquete>

 - Uma das formas de testa o erro é usando esse codigo na controller.
  
<blockquete>

            try
            {
                throw new Exception("Aconteceu algo horrivel");
            }
            catch(Exception e)
            {
                _logger.Error(e);
                throw;
            }
            return View();

</blockquete>

 - O log vai receber todos os erros e salvar.

 -

# Trabalhando com Filtros. (pipe)

- Um filtro pode ser confundido com um Middleware.

- Link recomendavel : https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1

- Existe filtros: autorização(já criamos), recursos, ação, exceção, resultado.

- Todos eles estão descrito no link da documentação.

- Nesse exemplo será mostrado um filtro de ação!

### exemplo na pratica

- Para isso é preciso criar um arquivo chamado "AuditoriaFilter", e implementar uma interface chamada "IActionFilter".

- Vamos implementar apenas o método "OnActionExecuted", no método construtor cria uma injeta o logger da aula passada.

- no "ActionExecutingContext" tem todas as informações do http, como o usuario,
então com isso cria uma logica para verificar se o urusrio está logado ou não.

- Cria uma variavel, e bota a informação de quem logou, com o kisslog armazena a informação da variavel.
  
<blockquete>

         public void OnActionExecuting(ActionExecutingContext context)
         {
                if(context.HttpContext.User.Identity.IsAuthenticated)
                {
                    var message = context.HttpContext.User.Identity.Name + " Acessou " +
                        context.HttpContext.Request.GetDisplayUrl();

                    _logger.Info(message);
                }
         }

</blockquete>

- Para ser um filtro Global, precisa ser registrado no StartUp.

- Não tem como instanciar a classe porq ela recebe uma injeção de dependencia.

- Então é criada uma injeção de dependencia!
  
<blockquete>
        
        services.AddControllersWithViews(options => 
        {
            options.Filters.Add(filterType:typeof(AuditoriaFilter));

        });

</blockquete>

- Injeção de dependencia.

<blockquete>

         services.AddScoped<AuditoriaFilter>();

</blockquete>

- Ver os outros tipos de filtros do link da documentação.

# Desenvolvendo uma aplicação MVC Core completa.

### Objetivos do módulo (Explicando Projeto)

- Apenas explica o projeto.

### Começando da forma fácil (Cria o projeto depois)

### Conclusão da forma fácil

# Setup da aplicação completa (Projeto: MinhaAppMvcCompleta)

 - Cria um projeto MVC DotnetCore 3.1

 - Cria uma biblioteca para projeto Business, e projeto Data.

 - No projeto Business cria as pastas: Models, Services, Notificacoes, Validations, Interfaces

 - No projeto Data cria as pastas: Context, Mappings, Repository

 - Cria as Modules.

# Mapeando as entidades para o banco de dados (API Fluent)

 - Cria uma classe de contexto chamado "MeuDbContext"

 - Instala o pacote do EF

<blockquete>

    Install-Package Microsoft.EntityFrameworkCore -Version 3.1.15

</blockquete>

 - Cria a referencia com as modules, e defina o contexto. 

<blockquete>

        public class MeuDbContext : DbContext
        {
            public MeuDbContext(DbContextOptions options) : base(options)
            {

            }

            public DbSet<Produto> Produtos { get; set; }

            public DbSet<Produto> Enderecos { get; set; }

            public DbSet<Produto> Fornecedores { get; set; }
        }

  </blockquete>

  - Instala o pacote adicional

<blockquete>

    Install-Package Microsoft.EntityFrameworkCore.Relational -Version 3.1.15

</blockquete>

- Cria uma classe chamada "ProdutoMapping", depois implementa a interface baseado em uma model, "IEntityTypeConfiguration<Produto>", esse arquivo deve ficar na pasta Mappings.

- Cria o mapeamento das Modules

<blockquete>

        public class ProdutoMapping : IEntityTypeConfiguration<Produto>
        {
            public void Configure(EntityTypeBuilder<Produto> builder)
            {
                // Define a chave primaria
                builder.HasKey(p => p.Id);

                // Define o campo como requirido, define o tipo da coluna            
                builder.Property(p => p.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                // Define o campo como requirido, define o tipo da coluna            
                builder.Property(p => p.Descricao)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

                // Define o campo como requirido, define o tipo da coluna            
                builder.Property(p => p.Imagem)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                builder.ToTable("Produtos");

            }
        }

</blockquete>

 - Cria o vinculo do mapeamento com o contexto.
 - Comando que pega o mapeamento definido na pasta Mappings.
 - Reflexion não é muito recomendado mas vai fazer uma vez só.
 
<blockquete>

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

</blockquete>

- Desativando o cascaiter, impede que a classe ao ser excluida, seja excluida junto!
- Faz uma pesquisa de relações dentro do modelBuild, pegando o tipo das entidades.
- selectMany cria uma lista, atraves das ForeignKeys, pega o comportamento a pós a exclusão (DeleteBehavior)
- Passa o "ClientSetNull". 
 <blockquete>

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

</blockquete>

- Uma garantia, caso seja necessario, define o minimo do varchar.

<blockquete>

        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

</blockquete>

- Faz a referencia da aplicação ao projeto Data de banco.

### Configuração do contexto na StratUp

- Cria uma configuração do contexto na StratUp

<blockquete>

    services.AddDbContext<MeuDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

</blockquete>
 
-Executa o comando para gerar a Migração

<blockquete>

        Add-Migration Initial -Verbose -Context MeuDbContext

</blockquete>
 
- instala o pacote

<blockquete>

    Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.15

</blockquete>
 
- Gera um Script-Migration em SQL, e salva na pasta do sql, para ter o codigo do sql.

<blockquete>

        Script-Migration -Context MeuDbContext

</blockquete>

- Cria as tabelas


<blockquete>

    Update-Database -Context MeuDbContext
 
</blockquete>

# Acessando o banco via repositórios

### Interface base

 - Cria uma interface chamada "IRepository" que recebe uma entidade usando o <T>, T representa generico.

 - Ela herda de IDisposable, sendo que todo "IDisposable" tem que ter o tipo Entity, que é uma classe
 que gera um id do tipo Guid.

 - A interface IDisposable, ela libera espaço de memoria.

<blockquete>

        public interface IRepository<T> : IDisposable where T : Entity
        {
            Task Adicionar(T entity);

            Task<T> ObterPorId(Guid id);

            Task<List<T>> ObterTodos();

            Task Atualizar(T Obj);

            Task Remover(Guid id);

            Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
        }

</blockquete>

 - Todo método vai ser asyncrono, por isso se bota o Task.

 - Quando tem apenas Task quer dizer que não retorna nada (void).

 - Expression: siginifica uma expressão LAMBDA, que trabalha com uma função,
   que compara a minha entidade com alguma coisa(predicate) des que ela retorna um bool.
 
 ### Criando Interface para cada entidade do sistema.

 - Define mais indentidades de métodos

<blockquete>

        public interface IProdutoRepository : IRepository<Produto>
        {
            Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);

            Task<IEnumerable<Produto>> ObterProdutosFornecedores();

            Task<Produto> ObterProdutoFornecedor(Guid id);
        }

</blockquete>

 ### Impelmentando a interface base

 - Percebe as mudanças de stado, retorna as mudanças com mais performace.
 - Deve sempre usar o await, para receber o valor do banco.
 - Declara alguns métodos como "virtual" para poder fazer um overaid nele.

<blockquete>
  
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<T> DbSet;

      
        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
        {          
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }     

        public async Task<T> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task Adicionar(T entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(T entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task Remover(Guid id)
        {            
            DbSet.Remove(new T { Id = id });
            await SaveChanges();
        }

        /// <summary>
        /// Salva no banco do contexto.
        /// Caso tenha algum tratamento, faça em apenas um método.
        /// </summary>        
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }

</blockquete>

 ### Implementando as interface das entidades

 - Exemplo do repositorio de produto, sendo implementado.

 - Herda a implementação do repositorio base, e implementa a interface do produto, porq tem métodos proprios.

<blockquete>


        public class ProdutoRepository : Repository<Produto>, IProdutoRepository
        {

            public ProdutoRepository(MeuDbContext context) : base(context){}

            public async Task<Produto> ObterProdutoFornecedor(Guid id)
            {
                // Include: é para incluir uma tabela na consulta.
                // Faz um inerJoin
                return await Db.Produtos.AsNoTracking()
                    .Include(f => f.Fornecedor)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
            {
                // Pega todos os produtos que aquele fornecedor tem vinculo,
                // retorna uma lista ordenada.
                return await Db.Produtos.AsNoTracking()
                    .Include(f => f.Fornecedor)
                    .OrderBy(p => p.Nome).ToListAsync();
            }

            public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
            {
                return await Buscar(p => p.FornecedorId == fornecedorId);
            }
        }

</blockquete>
 
# Mapeando as entidades em ViewModels com Automapper

 - Cria as ViewModel para cada entidade
 - Exemplo: 

<blockquete>

        public class ProdutoViewModel
        {
            [Key]
            public Guid Id { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [DisplayName("Fornecedor")]
            public Guid FornecedorId { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
            public string Nome { get; set; }

            [DisplayName("Descrição")]
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
            public string Descricao { get; set; }

            [DisplayName("Imagem do Produto")]
            public IFormFile ImagemUpload { get; set; }

            public string Imagem { get; set; }

            [Moeda]
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            public decimal Valor { get; set; }

            [ScaffoldColumn(false)]
            public DateTime DataCadastro { get; set; }

            [DisplayName("Ativo?")]
            public bool Ativo { get; set; }

            public FornecedorViewModel Fornecedor { get; set; }

            public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
        }

</blockquete>

 - Fornecedor

<blockquete>

    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        [DisplayName("Tipo")]
        public int TipoFornecedor { get; set; }
        
        public EnderecoViewModel Endereco { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }

</blockquete>

 - EnderecoViewModel

<blockquete>

    public class EnderecoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Estado { get; set; }

        [HiddenInput]
        public Guid FornecedorId { get; set; }
    }

</blockquete>
 
 ### Automaper

 - Instala o automaper 

<blockquete>

        Install-Package automapper.extensions.microsoft.dependencyinjection

</blockquete>
 
 - Configura o automaper na startUp

<blockquete>

        services.AddAutoMapper(typeof(Startup));

</blockquete>

 - Cria uma pasta chamada "AutoMapper" e um arquivo chamado "AutoMapperConfig"

 - Nesse arquivo é configurado o vinculo das videModel com as model.

<blockquete>

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }

    }

</blockquete>

# Scaffolding das Controllers e Views

 - Usa o Scaffolding para cria um controller mvc entity framework.
 - Antes de cria, deve primeiro comentar a linha do arquivo model que tenha o tipo "IFormFile".
 - Escolhe uma viewModel, depois um contexto, verifica o nome da controller.
 - É gerado as view automaticamente.
 - Injeta no controller o autoMapper, e o contexto.
  
<blockquete>

            private readonly IFornecedorRepository _IFornecedorRepository;
            private readonly IMapper _mapper;

            public FornecedoresController(IFornecedorRepository fornecedorRepository,
                IMapper mapper)
            {
                _IFornecedorRepository = fornecedorRepository;
                _mapper = mapper;
            }
 
</blockquete>

 - Com isso pode começar a fazer as consultas.
 - É necessario converter o objeto retornado do repositorio, para o tipo viewModel.
 - Se faz isso com autoMapper, é preciso converter porq a view trabalha com o objeto "FornecedorViewModel"
 
<blockquete>

        public async Task<IActionResult> Index()
        {
            return View( _mapper.Map<IEnumerable<FornecedorViewModel>>(await _IFornecedorRepository.ObterTodos()));
        }
        
</blockquete>

 - Com isso você deixa seu codigo mais limpo.

 - GetAll 

<blockquete>

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View( _mapper.Map<IEnumerable<FornecedorViewModel>>(await _FornecedorRepository.ObterTodos()));
        }

</blockquete>

 - GetById 

<blockquete>

        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(Guid id)
        { 
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
             
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

</blockquete>

- O Creat deve ter dois metodos, um get e outro post.

<blockquete>

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _FornecedorRepository.Adicionar(fornecedor);

            return RedirectToAction(nameof(Index));
        }

</blockquete>

- O adit a mesma coisa, usa um get e um post.

<blockquete>

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {   
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }
            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _FornecedorRepository.Atualizar(fornecedor);

            return RedirectToAction(actionName: "Index");            
        }

</blockquete>

- Delete usa um get e um post.

<blockquete>

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
               
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            await _FornecedorRepository.Remover(id);

            return RedirectToAction(actionName: "Index");
        }

</blockquete>

- Cria uma controller para o produto.

# Customização das Views
 
 - Cria uma parcial view de endereco.
  
 - Bota ela dentro da tela de create do fornecedor.

 - Ultiliza classes do bootstrap na tela de index.

 - Instala o font-awnso, na pasta lib, clica com o botão direto.

 - Add -> biblioteca do lado do cliente.

 - Arrasta e solta o arquivo css no arquivo de "_Layout" para gerar um link.

 - aplica as classes para por os icones.

<blockquete>

        <td class="text-right">
                <a class="btn btn-info" asp-action="Details" asp-route-id="@item.Id"><span class="fa fa-search"></span></a>
                <a class="btn btn-warning" asp-action="Edit" asp-route-id="@item.Id"><span class="fa fa-pencil-alt"></span></a>
                <a class="btn btn-danger" asp-action="Delete" asp-route-id="@item.Id"><span class="fa fa-trash"></span></a>
        </td>

</blockquete>
 
 - Na tela de create de produtos, não é usado o "ViewBag.FornecedorId", e sim uma outra forma de carregar,
 os fornecedores.

<blockquete>

            <div class="form-group">
                <label asp-for="FornecedorId" class="control-label"></label>
                <select asp-for="FornecedorId" class="form-control"
                        asp-items="@(new SelectList(Model.Fornecedores, dataValueField: "Id", dataTextField: "Nome"))">
                    <option></option>
                </select>
                <span asp-validation-for="FornecedorId" class="text-danger"></span>
            </div>

</blockquete>

 - Na tag form, deve ter mais uma informação , sobre os dados que vai ser salvo.
  
<blockquete>

        <form enctype="multipart/form-data" asp-action="Create">

</blockquete>

 - É colocada na parte de script uma validação manual, porq esse campo ele só recebe a imagem.
 
<blockquete>

          <script>
                // Botando o nome do arquivo na div.
                $("#ImagemUpload").change(function () {
                    $("#img_nome").text(this.files[0].name);
                    $("#img_nome")[0].style.display = 'block';
                });

                // Atribuindo uma validação manualmente.
                $("#ImagemUpload").attr("data-val", "true");
                $("#ImagemUpload").attr("data-val-required", "Preencha o campo Imagem");
           </script>

</blockquete>

 - ajuste o titulo e botão do formulario.

# Upload de imagem do produto

 - Cria um método que faz o upload da imagem antes de salva no banco.

<blockquete>

            private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
            {
                // Verifica se existe arquivo
                if (arquivo.Length <= 0) return false;

                // Cria um caminho
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);
        
                // Verifica se o arquivo é repetido.
                if(System.IO.File.Exists(path))
                {
                    ModelState.AddModelError(key: string.Empty, errorMessage: "Já existe um arquivo com este nome!");
                    return false;
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Faz a gravação no disco.
                    await arquivo.CopyToAsync(stream);
                }

                return true;
            }

</blockquete

 - Antes de chamar o método cria um prefixo.

 <blockquete>

            public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
            {
                // Cria um objeto vazio de "ProdutoViewModel" com uma lista de fornecedores.            
                produtoViewModel = await PopularFornecedores(produtoViewModel);

                // Se a ModelState não for valida recarrega a pagina.
                if (!ModelState.IsValid) return View(produtoViewModel);

                //upload do arquivo
                var imgPrefixo = Guid.NewGuid() + "_";
                if(!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoViewModel);
                }

                produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

                // Uso repositorio para adicionar o produto convertido.
                await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

                return RedirectToAction(actionName: "Index");
            }

</blockquete>
  
 - Para exibir na lista de produtos.

<blockquete>

        <td>
            <img src="~/imagens/@item.Imagem" alt="@item.Imagem" style="width: 100px; height: 100px"
                    class="img-fluid img-rounded"/>
        </td>

</blockquete>

# Customizando a edição do produto

 - Verifica se o id é o mesmo que tem no objeto.
 - Buscando os dados originais, salva na var "produtoAtualizacao".
 - Verificando se está valida a ModelState.
 - Verifica se tem imagem nova, se tiver salva no diretorio e troca.
 - Salva no banco o "produtoAtualizacao", uma forma segura de salvar informações.
 
<blockquete>

            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if(produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if(!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoViewModel);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            // Faz a atualização do produto.
            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            return RedirectToAction(actionName: "Index");

</blockquete>

 - No construtor do contexto bota uma configuração para evitar um bug aonde
 o Entity framework tenta usar o mesmo id de um objeto já salvo e não consegue atualizar os dados.

<blockquete>

            public MeuDbContext(DbContextOptions options) : base(options)
            {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                ChangeTracker.AutoDetectChangesEnabled = false;
            }

</blockquete>

# Globalizando a aplicação em pt-BR

 - Cria uma confgiração na StratUp sobre globalização.
  
<blockquete>

            var defaultCulture = new CultureInfo(name: "pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture },
            };
            app.UseRequestLocalization(localizationOptions);

</blockquete>

 - Cria um codigo JS para forçar o jQuery aceitar a globalização.

<blockquete>
        
            <script>
                $.validator.methods.range = function (value, element, param) {
                    var globalizedValue = value.replace(",", ".");
                    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
                };

                $.validator.methods.number = function (value, element) {
                    return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
                };

                $.validator.methods.date = function (value, element) {
                    var date = value.split("/");
                    return this.optional(element) || !/Invalid|NaN/.test(new Date(date[2], date[1], date[0]).toString());
                };
            </script>

</blockquete>

- Deixando as mensagem de erro padrão do ASP.net em pt-br.

- Cria uma pasta chamada "Configurations" e um arquivo chamado "MvcConfig.cs".

- Com ela uma configuração aonde traduz as mensagens padrão de erro do dotnet.

<blockquete>

    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo precisa ser preenchido.");

                //o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddRazorPages();

            return services;
        }
    }

</blockquete>


### Criando um atributo personalizado de Moeda.

- Cria 3 classes para configurar um atributo personalizado.

- Para ultilizar o atributo use apenas o nome "Moeda" na propriedade da viewModel.

<blockquete>

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

</blockquete>

- A primeira classe configura o atributo na parte backEnd.
- Cria uma classe chamada "MoedaAttribute", na pasta "Extensions".
- "Moeda" é o nome do atributo e "Attribute" é uma forma que o dotnet declara o atributo.
- A classe "ValidationAttribute" que é herdada define a classe "MoedaAttribute" como um atributo.

<blockquete>

    public class MoedaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }

            return ValidationResult.Success;
        }
    }

</blockquete>

- A segunda classe ele vincula regras de validação para o frontEnd.
- Cria um classe chamada "MoedaAttributeAdapter", no mesmo arquivo que tem a classe do atributo. 
- Devemos herdas a classe "AttributeAdapterBase" e passar o atributo que vai ser configurado para o backend.
- O método construtor é obrigatorio.
- Cria a validação e o tratamento de erro.

<blockquete>

    public class MoedaAttributeAdapter : AttributeAdapterBase<MoedaAttribute>
    {
        public MoedaAttributeAdapter(MoedaAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {

        }
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
        }
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "Moeda em formato inválido";
        }
    }

</blockquete>

- A terceira classe, é um provedor para o atributo, serve para a validação no frontEnd do atributo.
- Esse provedor é uma classe chamada "MoedaValidationAttributeAdapterProvider",
que herda a interface "IValidationAttributeAdapterProvider".
- Essa configuração é padrão, então basta copiar e colar com nome diferente para outros atributos.

<blockquete>

    public class MoedaValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is MoedaAttribute moedaAttribute)
            {
                return new MoedaAttributeAdapter(moedaAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }

</blockquete>

- Para finalizar devemos configurar uma injeção de dependencia na class StratUp.

<blockquete>

    services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

</blockquete>

### Editando o visual da exibição de valores moeda.

- Apenas bote um "pipe" do dotnet, troca o tipo de formatação.

- Antigo formato.

<blockquete>

        @Html.DisplayFor(modelItem => item.Valor)

</blockquete>

- Novo formato

<blockquete>

        @item.Valor.ToString(format:"C")

</blockquete>

- Reconhece qualquer pais, deacordo com a cultura passada.

# Finalizando o ajuste das Views

- Botando campo de area na descrição, na tela de create do produto.
- Botando o bootstrap na tela de detalhe do produto.
- Arruma o css das telas do CRUD.
- Cria um arquivo chamado "RazorExtensions" na pasta "Extensions".
- Máscara para editar a visualização de documento da pagina de fornecedores.

### Máscara dinamica.

<blockquete>

            public static class RazorExtensions
            {
                public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento)
                {
                    return tipoPessoa == 1
                        ? Convert.ToUInt64(documento).ToString(format: @"000\.000\.000\-00")
                        : Convert.ToUInt64(documento).ToString(format: @"00\.000\.000\/0000\-00");
                }
            }

</blockquete>

- Com esse método é possivel por uma mascara dinamica no documento.

<blockquete>

        <td>
            @this.FormataDocumento(item.TipoFornecedor, item.Documento)
        </td> 

</blockquete>

- Editando os detalhes de Fornecedor.
- Cria uma view que será uma parcialView.
- Escolha um nome com "_" no inicio.
- Depois um modelo que sera o modelo "List".
- Escolha um viewModel que será o "FornecedorViewModel".
- Dentro do "FornecedorViewModel" tem o objeto endereço, use ele paraexibir as informações.
- Depois um contexto, mas depois remova a referencia do contexto.
- o dotnet criara um view, você apenas edita o que realmente gostaria de exibir.

<blockquete>

        @model DevIO.App.ViewModels.FornecedorViewModel
           
        <div class="pt-3">
            <div>
                <hr />
                <h4>Endereço</h4>
            </div>

            @if (Model != null)
            {
                <table class="table table-hover">
                    <thead class="thead-dark">
                        <tr>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Logradouro)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Numero)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Complemento)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Bairro)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Cep)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Cidade)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Endereco.Estado)
                            </th>
                            <th></th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Logradouro)
                        </td>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Numero)
                        </td>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Complemento)
                        </td>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Bairro)
                        </td>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Cep)
                        </td>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Cidade)
                        </td>
                        <td>
                            @Html.DisplayFor(model => model.Endereco.Estado)
                        </td>
                        <td>
                            <a supress-by-action="Edit,ObterEndereco" asp-action="AtualizarEndereco" asp-route-id="@Model.Id" class="btn btn-info" data-modal="">
                                <span title="Editar" class="fa fa-pencil-alt"></span>
                            </a>
                        </td>
                    </tr>
                </table>
                }
        </div>

</blockquete>

- Usando a ParcialView de enderoço na tela de edição do fornecedor.

<blockquete>

        <div id="EnderecoTarget">
            <partial name="_DetalhesEndereco" />
        </div>

</blockquete>

### Lista de produtos
- Criando uma lista de produtos que vai ser reaproveitada.
- Copia a index de produto, e renomeia para "_ListaProdutos"
- No arquivo "_ListaProdutos" bota apenas a tabela.
- Nos botões bota as o controller, para ele saber que vem de produto.

<blockquete>

         <a class="btn btn-info" asp-controller="Produtos" 
         asp-action="Details" 
         asp-route-id="@item.Id">
         <span class="fa fa-search"></span></a>
                    
</blockquete>

- No na view "Index" dor produtos, bota uma parcialView.

<blockquete>

        <partial name="_ListaProdutos" />

</blockquete>

- No editar também bota a parcialView da lista de produtos, só que com outro caminho.
- Devemos alimentar essa parcialView, passando o model.

<blockquete>

        <partial name="../Produtos/_ListaProdutos" model="@Model.Produtos" />

</blockquete>

- Tratando a tela de detalhes do fornecedor.
- Bota um css pros botões, bota o tratamento do documento.
- Inclui a parcial view de endereço.
- Tratando a tela de deletar do fornecedor.

# Utilizando Modal Window de edição.

### Resumo

- Ideia: Quando o usuario entra na view de edit/delete/details do fornecedor, vai ter a opção de ver o endereço.
- Ele consegue ver porque a view tem uma partialView chamada "_DetalhesEndereco".

- No _DetalhesEndereco) tem um botão para editar, aonde é chamada uma modal via jQuery.
- Esse botão recebe o id atraves do "asp-route-id", e a action atraves do "asp-action".

- O Action que foi chamado se chama "AtualizarEndereco", que está dentro do controller "FornecedoresController".
- Esse action/método é executado, ele retorna um PartialViewResult, pelo método "PartialView".

- O "PartialView" recebe como parametro uma string que é o nome de uma partialView que vai ser a modal!
- E também recebe um objeto, obtido pelo metodo, que vai alimentar essa modal/partialView.

- Cria uma partialView chamada "_AtualizarEndereco", o form da partialView faz referencia a action,
usando asp-action="AtualizarEndereco", se usa uma outra partialView chamada "_Endereco" para invicar os forms.

- No arquivo js, o jQuery usa o método .load(), para passar todo o resultado do método "AtualizarEndereco", para
a div que tem o id "#myModalContent", essa div fica na view Edit, alem disso ela deixa o conteudo da div que tem o id
"#myModal" visivel. 

- Quando a modal sofre uma atualização, o método bindForm(), recebe uma url, fazendo com que a exibição do endereco
seja atualizado.
- partialView "_AtualizarEndereco" é mandada para "#EnderecoTarget" um id que fica na div da _DetalhesEndereco.

### Ideia em pratica

- Cria uma nova parcialView, chamada "_AtualizarEndereco"
- Boata as classes do bootstrap.
- O comando "data-dismiss="modal"" que faz a ação de fechar a modal.
- Bota uma partialView de endereço, que é o formulario.

<blockquete>

                @model DevIO.App.ViewModels.FornecedorViewModel
                @{ 
                    ViewData["Title"] = "Atualizar Endereço";
                }

                <div class="modal-header">
                    <h4 class="modal-title">@ViewData["Title"]</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hideen="true">x</span><span class="sr-only">Fechar</span>
                    </button>
                </div>

                <form asp-action="AtualizarEndereco">
                    <div class="modal-body">

                            <input type="hidden" asp-for="Endereco.FornecedorId" />
                            <input type="hidden" asp-for="Endereco.Id" />

                            <partial name="_Endereco" />

                            <div class="modal-footer">
                                <div class="col-md-offset-2 col-md-10">
                                    <input type="submit" value="Atualizar Endereço" class="btn btn-success" />
                                    <input type="button" class="btn btn-info" value="Fechar" data-dismiss="modal" />
                                </div>
                            </div>
                    </div>
                </form>

</blockquete>

- Cria o método/action, que vai popular os campos, para ser atualizados.
- Obtenha os dados para ser atualizados.
- Chama a PartialView, e alimenta apenas a parte do Endereco.

<blockquete>

            [Route("atualizar-endereco-fornecedor/{id:guid}")]
            public async Task<IActionResult> AtualizarEndereco(Guid id)
            {                
                var fornecedor = await ObterFornecedorEndereco(id);

                if (fornecedor == null)
                {
                    return NotFound();
                }                
                return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
            }

</blockquete>

 - Implementa um script na tela de "edit do fornecedor".
 - Esse script faz com que a modal abra.
 
<blockquete>
  
            @section Scripts {
            @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}

            <script>
                $(document).ready(function () {
                    AjaxModal();
                });
            </script>

</blockquete>

### Métodos do jQuery, para a modal funcionar.

 - Na pasta site.js do wwwtoot, recebe o medoto "SetModal()".
 - .on(): Quando o link que tiver o atributo "data-modal" for clickado, recebe a url e o id.
 - .load(): Pega os dados e bota aonde tem o id "#myModalContent".
 - .modal(): Comando que exibe a modal().
 - bindForm(): Método que pega os dados e alimenta a partialView "AtualizarEndereco"
 - Carrega o resultado aonde tem o id "#EnderecoTarget".
 - Quando o metodo do controller retorna uma PartialView, ele atualiza apenas a PartialView.
 - Isso é necessario para que ele atualiza apenas uma parte da view, atualiza apenas a partialView.

<blockquete>

        function SetModal() {

        $(document).ready(function () {
            $(function () {
                $.ajaxSetup({ cache: false }); //Configura chache

                $("a[data-modal]").on("click",
                    function (e) {
                        $('#myModalContent').load(this.href,
                            function () {
                                $('#myModal').modal({
                                    keyboard: true
                                },
                                    'show');
                                bindForm(this);
                            });
                        return false;
                    });
            });
        });
        }

        function bindForm(dialog) {
        $('form', dialog).submit(function () {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.success) {
                        $('#myModal').modal('hide'); // Esconde a modal.
                        $('#EnderecoTarget').load(result.url); // Carrega o resultado HTML para a div demarcada.
                    } else {
                        $('#myModalContent').html(result);
                        bindForm(dialog);
                    }
                }
            });

            SetModal();
            return false;
        });
}

</blockquete>

### Método para atualizar os dados do endereço na modal.

- Cria um método actionResult post para atualizar no proprio controller do fornecedor para o endereço.
- De o nome de "AtualizarEndereco".
- Remove alguns itens da "ModelState", porq a modal só avalia apenas o objeto endereço.
- Verifica de a "ModelState" está ok.
- Pega os dados usando o repositorio.
- converte em uma url para o Json e retorna dentro de um objeto.
- Para criar essa url, é preciso criar outro método chamado "ObterEndereco".
- Esse método retorna uma partialView chamada "_DetalhesEndereco".

<blockquete>

        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            // Obtenha os dados para ser atualizados.
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }
            // chama a PartialView, e alimenta apenas a parte do Endereco.
            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedorViewModel);

            await _enderecoRepository.Atualizar(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));
                        
            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
            return Json(new { success = true, url });
        }

</blockquete>

# Utilizando Ajax para preenchimento do CEP

- Com jQuery busca o CEP.
- .val(): atribui valor para o id que foi selecionado.
- .blur(): evento quando perde o foco é executado.

<blockquete>
 
                function BuscaCep() {
                    $(document).ready(function () {

                        function limpa_formulário_cep() {
                            // Limpa valores do formulário de cep.
                            $("#Endereco_Logradouro").val("");
                            $("#Endereco_Bairro").val("");
                            $("#Endereco_Cidade").val("");
                            $("#Endereco_Estado").val("");
                        }

                        //Quando o campo cep perde o foco.
                        $("#Endereco_Cep").blur(function () {

                            //Nova variável "cep" somente com dígitos.
                            var cep = $(this).val().replace(/\D/g, '');

                            //Verifica se campo cep possui valor informado.
                            if (cep != "") {

                                //Expressão regular para validar o CEP.
                                var validacep = /^[0-9]{8}$/;

                                //Valida o formato do CEP.
                                if (validacep.test(cep)) {

                                    //Preenche os campos com "..." enquanto consulta webservice.
                                    $("#Endereco_Logradouro").val("...");
                                    $("#Endereco_Bairro").val("...");
                                    $("#Endereco_Cidade").val("...");
                                    $("#Endereco_Estado").val("...");

                                    //Consulta o webservice viacep.com.br/
                                    $.getJSON("htt ps://viacep.com.br/ws/" + cep + "/json/?callback=?",
                                        function (dados) {

                                            if (!("erro" in dados)) {
                                                //Atualiza os campos com os valores da consulta.
                                                $("#Endereco_Logradouro").val(dados.logradouro);
                                                $("#Endereco_Bairro").val(dados.bairro);
                                                $("#Endereco_Cidade").val(dados.localidade);
                                                $("#Endereco_Estado").val(dados.uf);
                                            } //end if.
                                            else {
                                                //CEP pesquisado não foi encontrado.
                                                limpa_formulário_cep();
                                                alert("CEP não encontrado.");
                                            }
                                        });
                                } //end if.
                                else {
                                    //cep é inválido.
                                    limpa_formulário_cep();
                                    alert("Formato de CEP inválido.");
                                }
                            } //end if.
                            else {
                                //cep sem valor, limpa formulário.
                                limpa_formulário_cep();
                            }
                        });
                    });
                }

</blockquete>


# Organizando e otimizando a estrutura da App

### Configurações que já foramexplicadas.

- Isolou o codigo da startUp que envolve: Injeção de dependencia, Identity, Validações, Globalização.
- Criou no contrutor da startUp e criou outros arquivos "appsettings.json", para criar outros ambientes,
finalizando a configuração no arquivo "launchSettings.json"
- Configurou um "EmailTagHelper".

# Roteamento inteligente

- Cria nome para as rotas, de cada action das controller.
- Exemplo: 

<blockquete>

        [Route("lista-de-fornecedores")]
 
</blockquete>

# Validando as entidades de negócio

- Vamos validar os dados do lado da camada de negocios.
- É diferente da validação da camada de cliente, que serve apenas para filtrar.
- Não e seguro depender do JS, porq ele pode ser desligado.
- No vanegador na area de inspeção usa o comando crtl+shift+p, e com isso desabilitar o JS.
- Por isso essa validação pelo cliente não é tão segura.

### Criando classes de serviços.

- Cria uma classe chamada "BaseService","FornecedorService","ProdutoService" 
na pasta "service" da camada de negocio.

- Cria duas interfaces chamada "IFornecedorService" e "IProdutoService".

### Conhecendo o FluentValidation

- Serve para validar as entidade, na camada da regra de negocio.
- https://docs.fluentvalidation.net/en/latest/aspnet.html
- É criado classes que tem validações de cada entidade.

<blockquete>

    Install-Package FluentValidation
 
</blockquete>

- Bota a pasta "Validations" na pasta "Models".
- Na pasta Validtions da camada de negocios, cria uma arquivo chamado: FornecedorValidations.
- Essa classe herda a classe "AbstractValidator<Fornecedor>", é passado o objeto que vai ser validado para ela.
- Pode usar o "When" para definir uma situação, quando é uma pessoa fisica ou pessoa juridica, aciona uma ação.

<blockquete>

        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(min: 2, max: 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });
        }

</blockquete>

### ver depois com calma
- A classe "CpfValidacao" é criada separada na pasta "Documentos", dentro da pasta "Validations" 
é um arquivo com muitas classes e varias logicas.

- Esse arquivo auxilia na hora de validar a logica nas classes que valida as entidades, que fica na pasta validations.

- Explicação do operador "Equal"!
- http://www.macoratti.net/18/07/c_equalsop1.htm
- https://www.youtube.com/watch?v=I5EUwaHI75U&ab_channel=GabrielArtigas%28CursoseTutoriais%29


### Configurando Notificação no contrutor da classe "BaseService"
- A classe BaseService, recebe métodos que gerencia as mensagens de errro!
- E métodos que valida apenas passando classes de validação e a entidade!
- continua no proximo video!

# Regras de negócio e eventos de notificações.
- Na classe "FornecedorService" cria uma injeção de dependencia de "IFornecedorRepository" e "IEnderecoRepository".
- No método adicionar, cria um if para verificar se existe o fornecedor cadastrado com o documento informado, 
usando o método "Buscar" do repositorio do fornecedor. 
- No método de atualizar veifica se existe o documento cadastrado e se o usuario é diferente do que já existe,
porque tem que ser um fornecedor cadastrado.
- No método excluir, verifica se o fornecedor tem produtos, caso tenha ele não vai ser excluido.

<blockquete>

        public class FornecedorService : BaseService, IFornecedorService
        {
            private readonly IFornecedorRepository _fornecedorRepository;
            private readonly IEnderecoRepository _enderecoRepository;

            public FornecedorService(IFornecedorRepository fornecedorRepository,
                                     IEnderecoRepository enderecoRepository)
            {
                _fornecedorRepository = fornecedorRepository;
                _enderecoRepository = enderecoRepository;
            }


            public async Task Adicionar(Fornecedor fornecedor)
            {
                // validar o estado da entidade.            
                if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                    && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

                // verificar se existe fornecedor com o mesmo documento.
                if(_fornecedorRepository.Buscar(predicate: f=>f.Documento == fornecedor.Documento).Result.Any())
                {
                    Notificar(mensagem: "Já existe um fornecedor com este documento informado.");
                    return;
                }

                await _fornecedorRepository.Adicionar(fornecedor);
            }

            public async Task Atualizar(Fornecedor fornecedor)
            {
                if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

                // Verifica se a atualização tem o documento cadastrado, e se é de um fornecedor diferente do que foi achado.
                if(_fornecedorRepository.Buscar(predicate:f => f.Documento == fornecedor.Documento && 
                f.Id != fornecedor.Id).Result.Any())
                {
                    Notificar(mensagem: "Já existe um fornecedor com este documento informado.");
                    return;
                }
                await _fornecedorRepository.Atualizar(fornecedor);
            }

            public async Task AtualizarEndereco(Endereco endereco)
            {
                if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

                await _enderecoRepository.Atualizar(endereco);
            }
         

            public async Task Remover(Guid id)
            {
                // Caso o fornecedor tenha produtos, não será excluido.
                if(_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
                {
                    Notificar(mensagem: "O fornecedor possui produtos cadastrados!");
                    return;
                }

                await _fornecedorRepository.Remover(id);
            }

            public void Dispose()
            {
                _fornecedorRepository?.Dispose();
                _enderecoRepository?.Dispose();
            }
        }
 
</blockquete>

- Termina de implementa "ProdutoService".
- Herda a interface "IDisposable" na interface IFornecedorService e IProdutoService.
- Implementa o IDisposable nas classes de serviço, chamando o dispose do repositorio.

<blockquete>

        public class ProdutoService : BaseService, IProdutoService
        {
            private readonly IProdutoRepository _produtoRepository;

            public ProdutoService(IProdutoRepository produtoRepository)
            {
                _produtoRepository = produtoRepository;
            }

            public async Task Adicionar(Produto produto)
            {
                if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

                await _produtoRepository.Adicionar(produto);
            }

            public async Task Atualizar(Produto produto)
            {
                if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

                await _produtoRepository.Atualizar(produto);
            }
               
            public async Task Remover(Guid id)
            {
                await _produtoRepository.Remover(id);
            }

            public void Dispose()
            {
                _produtoRepository?.Dispose();
            }
        }
 
</blockquete>

### Gerenciando as notificações(da suporta a BaseService)

- Cria uma interface chamada "INotificador", mas antes cria uma classe chamada "Notificacao",
dentro de uma pasta chamada "Notificacoes".
- Essa interface e classe serve para injeta no BaseService, responsavel por passar as mensagens.

<blockquete>

        public interface INotificador
        {
            bool TemNotificacao();
            List<Notificacao> ObterNotificacoes();
            void Handle(Notificacao notificacao);
        }
 
</blockquete>

- A classe Notificacao ela serve para ser instanciada na hora de adicionar uma notificação na classe "BaseService",
quando for usar o método "Handle", atravez da interface "INotificador" que foi injetada.

<blockquete>

            public class Notificacao
            {
                public Notificacao(string mensagem)
                {
                    Mensagem = mensagem;
                }
                public string Mensagem { get; }
            }

</blockquete>

- Classe "Notificador" implementa a interface "INotificador".

<blockquete>

            public class Notificador : INotificador
            {
                // Uma lista de notificações.
                private List<Notificacao> _notificacoes;
                
                public Notificador()
                {
                    // O construtor recebe uma lista de notificações vazio.
                    _notificacoes = new List<Notificacao>();
                }

                public void Handle(Notificacao notificacao)
                {
                    // Adiciona a notificação na lista. 
                    _notificacoes.Add(notificacao);
                }

                public List<Notificacao> ObterNotificacoes()
                {
                    // Retorna a lista de notificação.
                    return _notificacoes;
                }

                public bool TemNotificacao()
                {
                    // Retorna se existe notificações na lista.
                    return _notificacoes.Any();
                }
            }
 
</blockquete>

- Configura a injeção de dependencia do "INotificador", "IFornecedorService", "IProdutoService", 
no arquivo "DependencyInjectionConfig".

<blockquete>

        services.AddScoped<INotificador, Notificador>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IProdutoService, ProdutoService>();
 
</blockquete>

- É preciso injetar no construtor do "FornecedorService" e "ProdutoService" a interface "INotificador",
e passar para a base!
-
-
<blockquete>

 public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador): base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }
         
</blockquete>

### Modificando os métodos (adicionar,atualizar,delete) da controler!

- Injeta os serviço no construtor, e modifica os métodos atualizar, adicionar, deletar e alterar endereço.

### Tratando as notificações nos construtores.

- Injeta a interface "INotificador" na classe "BaseController".

<blockquete>

        public class BaseController : Controller
        {
            private readonly INotificador _notificador;

            protected BaseController(INotificador notificador)
            {
                _notificador = notificador;
            }

            protected bool OperacaoValida()
            {
                return !_notificador.TemNotificacao();
            }
        }
 
</blockquete>

- Toda o metodo do controller que tem o serviço, bota o if verificando se tem notificações.

<blockquete>

        if (!OperacaoValida()) return View(produtoViewModel);
 
</blockquete>

- Cria uma component chamada "SummaryViewComponent" na pasta de extensions.

<blockquete>

            public class SummaryViewComponent : ViewComponent
            {
                private readonly INotificador _notificador;

                public SummaryViewComponent(INotificador notificador)
                {
                    _notificador = notificador;
                }

                public async Task<IViewComponentResult> InvokeAsync()
                {
                    var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
                    notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

                    return View();
                }
            }
 
</blockquete>

- Essa classe faz com que as notificações apareça na view atravez do "asp-validation-summary", é
uma taghelper que exibe as notificações, troque o valor para All.

- ModelOnly : Apenas erros da modelState.
- All: todos os erros.

<blockquete>

            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div asp-validation-summary="All" class="text-danger"></div>
 
</blockquete>

- Cria uma pasta chamada "Components" dentro dela uma pasta "Summary" e uma view chamada "Default.cshtml".
- Essa é a visualização das notificações.
- Exibe mensagens de erros, sucess e sucess com tempdata

<blockquete>

            @if (ViewData.ModelState.ErrorCount > 0)
            {
                <div style="padding-top: 15px;"></div>

                <div class="alert alert-danger">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <h3 id="msgRetorno" style="padding-top: 20px">Opa! Algo deu errado :(</h3>
                    <div style="padding-top: 20px" asp-validation-summary="ModelOnly" class="text-danger"></div>
                </div>
            }

            @if (!string.IsNullOrEmpty(ViewBag.Sucesso))
            {
                <div style="padding-top: 15px"></div>

                <div id="msg_box" class="alert alert-success">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <h3 id="msgRetorno">@ViewBag.Sucesso</h3>
                </div>
            }

            @if (TempData["Sucesso"] != null)
            {
                <div style="padding-top: 15px"></div>

                <div id="msg_box" class="alert alert-success">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <h4 id="msgRetorno">@Html.Raw(TempData["Sucesso"].ToString())</h4>
                </div>
            }
 
</blockquete>

- Na view de creat do fornecedor, bota uma taghelper do novo component criad.
- Ele trata os outros erros!

<blockquete>

        <vc:Summary></vc:Summary>

</blockquete>

- Não importa quantas vezes foi injetado a interface "INotificador" em camadas diferentes, 
sempre vai ter as mesma mensagem, durante o processamento, porque foi configurado com "AddScoped".

- Nos caso de sucesso, como o de excluir.

<blockquete>

            TempData["Sucesso"] = "Produto excluido com sucesso!";

</blockquete>

#  Segurança e técnicas inéditas com Identity
- Cria um usuario, depois cria 2 clans com o id do usuario.
- Uma claimType de "Produto" e outra de "Fornecedor".
- As ClaimValue deve ter "Adicionar,Editar,Exluir".

### Configurando as autenticação.
- Cria um arquivo cs na pasta Extensions, chamada "CustomAuthorization".
- Essas classe ajuda configurar autenticação.
- A primeira classe tem o método static "ValidarClaimsUsuario" 
que valida as claim's do usuario, verifica se ele possui a autorização e as claim's.
- A classe "ClaimsAuthorizeAttribute" é a declaração de uma tributo que recebe as claimName e claimValue.
- A classe "RequisitoClaimFilter" verifica se está autenticado, e se tem as claim's esperada.

### configurando os controller
- Bota o metadado "[Authorize]" em cada controller.
- Os métodos de index e detalhes bota como "[AllowAnonymous]".
- Cada método recebe o metadado de "ClaimsAuthorize" correspondido!!
- exemplo, passa o claimType e claimValue.
<blockquete>

     [ClaimsAuthorize("Fornecedor", "Editar")]

</blockquete>

### Escondendo funcionabilidade da view com razon.(criando uma tagHelper de uma outra forma)
- No botão de excluir que fica na lista de fornecedores, aplica os taghelper:
- Asssim ele nãi exibe caso não tenha o tipo e valor da claim.
- Precisa também criar umas extenções do taghelper.

<blockquete>

             <a class="btn btn-danger" 
                   supress-by-claim-name="Fornecedor"
                   supress-by-claim-value="Excluir"
                   asp-action="Delete" asp-route-id="@item.Id">
                <span class="fa fa-trash"></span></a>

</blockquete>

- Na pasta de extensions é criado um arquivo chamado: "ApagaElementoByClaimTagHelper.cs".
- Nesse arquivo tem 2 classe 

- A primeira é a "ApagaElementoTagHelper", que herda a classe "TagHelper".
- E acima da classe é definida quais tags ira aceitar essa TagHelper e o nome dela.
- Cria dois atributos que identifica o nome e valor das claim's.
- Toda logica fica dentro do método "Process", aonde é processada essa tagHelper.
- Dentro desse método você deve validar o contexto e o output, caso seja null retorna um "ArgumentNullException".
- O contexto é oque entra no taghelper, e o output é tudo que sai.
- O "IHttpContextAccessor" é injetado no método construtor, para poder pegar o contexto, 
é um meio de acessar o contexto por http(do asp.net core).
- Usa o método static "ValidarClaimsUsuario" da classe CustomAuthorization, para valida
as claim's do usuario, verifica se ele possui a autorização e as claim's.
- Bota o valor do método em uma variavel, se tiver acesso não retorna nada.
- Caso não tenha não gere a tag usando o comando " output.SuppressOutput();"

<blockquete>

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));
                if (output == null)
                    throw new ArgumentNullException(nameof(output));

                var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

                if (temAcesso) return;

                output.SuppressOutput();
            }

</blockquete>

- Foi usado no botão excluir da lista de fornecedores.

### Criaum uma tagHelp que desabilita link

- Cria um arquivo chamado "DesabilitaLinkTagHelper.cs" na pasta extensions.
- Faz toda configuração dita anteriormente, só que caso ele não tenha a autorização e as claim's pedidas,
você deve remover tudo que tem no "href" do link e modificar o title e por um style.

<blockquete>

            [HtmlTargetElement("*", Attributes = "disable-by-claim-name")]
            [HtmlTargetElement("*", Attributes = "disable-by-claim-value")]
            public class DesabilitaLinkByClaimTagHelper : TagHelper
            {
                private readonly IHttpContextAccessor _contextAccessor;

                public DesabilitaLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
                {
                    _contextAccessor = contextAccessor;
                }

                [HtmlAttributeName("disable-by-claim-name")]
                public string IdentityClaimName { get; set; }

                [HtmlAttributeName("disable-by-claim-value")]
                public string IdentityClaimValue { get; set; }

                public override void Process(TagHelperContext context, TagHelperOutput output)
                {
                    if (context == null)
                        throw new ArgumentNullException(nameof(context));
                    if (output == null)
                        throw new ArgumentNullException(nameof(output));

                    var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

                    if (temAcesso) return;

                    output.Attributes.RemoveAll("href");
                    output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
                    output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));
                }
            }

</blockquete>

- Foi usado no botão editar da lista de fornecedores.

### Escondendo ou mostrando algo, com base na action.

- Com base no nome da ação, quer mostrar ou não alguma coisa.
- Nesse tratamento de taghelp tem uma unica propriedade chamada "ActionName".
- Sobre escreve o método "Pross", igual nas outras configurações.
- Com o "_contextAccessor" você consegue acessar uma coleção de dados da rota, usando o método "GetRouteData()".
- Com isso você busca a chave "action", assim você pega a action dentro de um request.
- Caso tenha o valor retorna nada, se não , não gera o elemento.

<blockquete>

            [HtmlTargetElement("*", Attributes = "supress-by-action")]
            public class ApagaElementoByActionTagHelper : TagHelper
            {
                private readonly IHttpContextAccessor _contextAccessor;

                public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
                {
                    _contextAccessor = contextAccessor;
                }

                [HtmlAttributeName("supress-by-action")]
                public string ActionName { get; set; }

                public override void Process(TagHelperContext context, TagHelperOutput output)
                {
                    if (context == null)
                        throw new ArgumentNullException(nameof(context));
                    if (output == null)
                        throw new ArgumentNullException(nameof(output));

                    var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

                    if (ActionName.Contains(action)) return;

                    output.SuppressOutput();
                }
            }

</blockquete>

- Foi usando no botão que atualiza o endenreço, botão que está na partialview "_DetalhesEndereco". 
- Caso não seja a action "edit" o botão não é exibido.

# Tratamento de erros


-
-
-

<blockquete>

</blockquete>
-
-
-

<blockquete>

</blockquete>
-
-
-

<blockquete>

</blockquete>








