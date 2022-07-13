using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using System.Net;

namespace Modelo.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IBaseRepository _baseRepository;
        public ProdutoRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AtualizarProduto(Produto produto)
        {
            try 
            {
                var produtoEntity = ConverterProdutoParaProdutoEntity(produto);

                await _baseRepository.AtualizarEntidade(produtoEntity, typeof(ProdutoEntity).Name);
            }         
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InserirProduto(Produto produto)
        {
            try
            {
                produto.Id = Guid.NewGuid();

                var produtoEntity = ConverterProdutoParaProdutoEntity(produto);

                await _baseRepository.InserirEntidade(produtoEntity, typeof(ProdutoEntity).Name);               

            }          
            catch(Exception ex)
            {
                throw ex;
            }       
         
        }

        public async Task<Produto> ObterProduto(string id)
        {            
            try 
            {
                var produtoEntity = await _baseRepository.BuscarEntidade<ProdutoEntity>(typeof(ProdutoEntity).Name, id, typeof(ProdutoEntity).Name);          
                return ConverterProdutoEntityParaProduto(produtoEntity);
            }         
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            try
            {
                var produtosEntities = await _baseRepository.BuscarTodasEntidadesAsync<ProdutoEntity>(typeof(ProdutoEntity).Name);
                return ConverteProdutosEntitiesParaProdutos(produtosEntities);
            }           
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AtualizarProdutos(List<Produto> produtos)
        {
            try           
            {
                foreach (var produto in produtos)
                {
                    await AtualizarProduto(produto);
                }

            }catch(Exception ex)
            {
                throw ex;
            }         
      
        }

        private ProdutoEntity ConverterProdutoParaProdutoEntity(Produto produto)
        {         
            return new ProdutoEntity
            {
                PartitionKey = typeof(ProdutoEntity).Name,
                RowKey = produto.Id.ToString(),

                Id = produto.Id.ToString(),
                Nome = produto.Nome,
                Preco = produto.Preco,
                Descricao = produto.Descricao,
                QtdEstoque = produto.Qtd
            };

        }

        private Produto ConverterProdutoEntityParaProduto(ProdutoEntity produtoEntity)
        {
            if(produtoEntity == null)
            {
                return null;
            }

            return new Produto 
            {
                Id = Guid.Parse(produtoEntity.Id),
                Nome = produtoEntity.Nome,
                Preco = produtoEntity.Preco,
                Descricao = produtoEntity.Descricao,
                Qtd = produtoEntity.QtdEstoque
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
