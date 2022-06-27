using Microsoft.WindowsAzure.Storage.Table;
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

        public bool AtualizarProduto(Produto produto)
        {
            var produtoEntity = ConverterProdutoToProdutoEntity(produto);

            var result = _baseRepository.AtualizarEntidade(produtoEntity, typeof(ProdutoEntity).Name);

            if (result.Result != null) return true;

            return false;
        }

        public bool InserirProduto(Produto produto)
        {
            var produtoEntity = ConverterProdutoToProdutoEntity(produto);

            var result = _baseRepository.InserirEntidade(produtoEntity, typeof(ProdutoEntity).Name);

            if (result.Result != null) return true;

            return false;
        }

        public async Task<Produto> ObterProduto(string id)
        {

            var produtoEntity = await _baseRepository.BuscarEntidade<ProdutoEntity>(typeof(ProdutoEntity).Name, id, typeof(ProdutoEntity).Name);

            return ConverterProdutoEntityToProduto(produtoEntity);               

        }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            var produtosEntites = await _baseRepository.BuscarTodasEntidadesAsync<ProdutoEntity>(typeof(ProdutoEntity).Name);
            return ConverteProdutosEntitiesToProdutos(produtosEntites);

        }

        private ProdutoEntity ConverterProdutoToProdutoEntity(Produto produtoEntity)
        {
            return new ProdutoEntity
            {
                PartitionKey = typeof(ProdutoEntity).Name,
                RowKey = produtoEntity.Id.ToString(),

                Id = produtoEntity.Id.ToString(),
                Nome = produtoEntity.Nome,
                Preco = produtoEntity.Preco,
                Descricao = produtoEntity.Descricao,
                QtdEstoque = produtoEntity.QtdEstoque
            };

        }
        private Produto ConverterProdutoEntityToProduto(ProdutoEntity produtoEntity)
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

        private List<Produto> ConverteProdutosEntitiesToProdutos(List<ProdutoEntity> produtosEntities)
        {
            var produtos = new List<Produto>();

            foreach (var item in produtosEntities)
            {
               produtos.Add(ConverterProdutoEntityToProduto(item));
            }

            return produtos;
        }


     
    }
}
