using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador): base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }


        public async Task Adicionar(Fornecedor fornecedor)
        {
            // validar o estado da entidade.            
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

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
}
