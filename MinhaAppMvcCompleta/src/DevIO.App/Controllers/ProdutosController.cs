using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DevIO.App.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRepository _fornecedorRepository;        
        private readonly IMapper _mapper;

        //Injeção de dependencia dos repositorios e do Automapper
        public ProdutosController(
            IProdutoRepository produtoRepository,
            IProdutoService produtoService,
            IFornecedorRepository fornecedorRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // GET: Produtos
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {    
            // Pega todos os produtos e seus fornecedores, converte para um IEnumerable "ProdutoViewModel"
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        // GET: Produtos/Details/5
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            // Pega o produto que tem o id especifico, exiba a tela apenas se ele existir.
            var produtoViewModel = await ObterProduto(id);
            if (produtoViewModel == null)
            {
                return NotFound();
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            // Cria um objeto vazio de "ProdutoViewModel" com uma lista de fornecedores.
            var ProdutoViewModel = await PopularFornecedores(new ProdutoViewModel());
            return View(ProdutoViewModel);
        }

        // POST: Produtos/Create
        [Route("novo-produto")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(actionName: "Index");
        }

        // GET: Produtos/Edit/5
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {            
            //Obtem o produto que vai ser editado
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }
            
            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        //[ValidateAntiForgeryToken]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]        
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            //Verifica se o id é o mesmo que tem no objeto
            if (id != produtoViewModel.Id) return NotFound();

            // Buscando os dados originais.
            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            // Verificando se está valida a ModelState
            if (!ModelState.IsValid) return View(produtoViewModel);

            // Verifica se tem imagem nova.
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
            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(actionName: "Index");
        }

        // GET: Produtos/Delete/5
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Obtem o produto que vai ser deletado
            var produto = await ObterProduto(id);

            if(produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        //[ValidateAntiForgeryToken]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]       
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // Obtem o produto que vai ser deletado
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }
            // Deleta o produto usando o repositorio.
            await _produtoService.Remover(id);

            if (!OperacaoValida()) return View(produto);
                        
            TempData["Sucesso"] = "Produto excluido com sucesso!";

            return RedirectToAction(actionName: "Index");
        }

                
        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto =  _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {            
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

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

    }
}
