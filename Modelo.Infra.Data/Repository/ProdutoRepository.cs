using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;

namespace Modelo.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IBaseRepository _baseRepository;
        public ProdutoRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> AtualizarProduto(Produto produto)
        {
            var produtoEntity = ConverterProdutoParaProdutoEntity(produto);

            var result = await _baseRepository.AtualizarEntidade(produtoEntity, typeof(ProdutoEntity).Name);

            if (result.Result != null) return true;

            return false;
        }

        public async Task<bool> InserirProduto(Produto produto)
        {
            var produtoEntity = ConverterProdutoParaProdutoEntity(produto);

            var result = await _baseRepository.InserirEntidade(produtoEntity, typeof(ProdutoEntity).Name);

            if (result.Result != null) return true;

            return false;
        }

        public async Task<Produto> ObterProduto(string id)
        {

            var produtoEntity = await _baseRepository.BuscarEntidade<ProdutoEntity>(typeof(ProdutoEntity).Name, id, typeof(ProdutoEntity).Name);

            return ConverterProdutoEntityParaProduto(produtoEntity);               

        }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            var produtosEntites = await _baseRepository.BuscarTodasEntidadesAsync<ProdutoEntity>(typeof(ProdutoEntity).Name);
            return ConverteProdutosEntitiesParaProdutos(produtosEntites);

        }

        public async Task<bool> AtualizarProdutos(List<Produto> produtos)
        {
            try
            {
                foreach (var produto in produtos)
                {
                    await AtualizarProduto(produto);

                }

                return true;

            }catch(Exception ex)
            {
                return false;
            }         
      
        }

        private ProdutoEntity ConverterProdutoParaProdutoEntity(Produto produto)
        {
            produto.Id = Guid.NewGuid();

            return new ProdutoEntity
            {
                PartitionKey = typeof(ProdutoEntity).Name,
                RowKey = produto.Id.ToString(),

                Id = produto.Id.ToString(),
                Nome = produto.Nome,
                Preco = produto.Preco,
                Descricao = produto.Descricao,
                QtdEstoque = produto.QtdEstoque
            };

        }

        private Produto ConverterProdutoEntityParaProduto(ProdutoEntity produtoEntity)
        {
            return new Produto 
            {
                Id = Guid.Parse(produtoEntity.Id),
                Nome = produtoEntity.Nome,
                Preco = produtoEntity.Preco,
                Descricao = produtoEntity.Descricao,
                QtdEstoque = produtoEntity.QtdEstoque
            };
                       
        }

        private List<Produto> ConverteProdutosEntitiesParaProdutos(List<ProdutoEntity> produtosEntities)
        {
            var produtos = new List<Produto>();

            foreach (var item in produtosEntities)
            {
               produtos.Add(ConverterProdutoEntityParaProduto(item));
            }

            return produtos;
        }
        
    }
}
