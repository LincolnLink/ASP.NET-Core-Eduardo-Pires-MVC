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