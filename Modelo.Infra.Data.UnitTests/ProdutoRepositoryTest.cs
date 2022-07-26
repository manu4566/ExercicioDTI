using AutoFixture;
using FluentAssertions;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using Modelo.Infra.Data.Repository;
using Moq;

namespace Modelo.Infra.Data.UnitTests
{
    public class ProdutoRepositoryTest
    {
        private IFixture _fixture;

        private Mock<IBaseRepository> _baseRepository;
       
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _baseRepository = new Mock<IBaseRepository>();
           
        }

        [Test] 
        public void AtualizaProduto()
        {
            _baseRepository
             .Setup(mock => mock.AtualizarEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()))
             .Verifiable();          

            var appService = InstanciarProdutoRepository();

            var retorno = appService.AtualizarProduto(_fixture.Create<Produto>());

            _baseRepository.Verify(mock => mock.AtualizarEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()), Times.Once);
            retorno.Should().NotBe(It.IsAny<Exception>());
        } 

        [Test]
        public void AtualizaListaDeProdutos()
        {
            var produtos = _fixture.CreateMany<Produto>().ToList();

            _baseRepository
               .Setup(mock => mock.AtualizarEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()))
               .Verifiable();

            var appService = InstanciarProdutoRepository();     

            var retorno = appService.AtualizarProdutos(produtos);

            _baseRepository.Verify(mock => mock.AtualizarEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()),Times.Exactly(produtos.Count));
            retorno.Should().NotBe(It.IsAny<Exception>());
        }

        [Test]
        public void InsereProduto()
        {
            _baseRepository
             .Setup(mock => mock.InserirEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()))
             .Verifiable();

            var appService = InstanciarProdutoRepository();

            var retorno = appService.InserirProduto(_fixture.Create<Produto>());

            _baseRepository.Verify(mock => mock.InserirEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()), Times.Once);
            retorno.Should().NotBe(It.IsAny<Exception>());
        }     

        [Test]
        public async Task BuscaProdutoERetornaProduto()
        {
            var produtoEntity = _fixture.Build<ProdutoEntity>()
                .With(produto => produto.Id, Guid.Empty.ToString())
                .Create();

            _baseRepository
             .Setup(mock => mock.BuscarEntidade<ProdutoEntity>( It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
             .ReturnsAsync(produtoEntity);

            var appService = InstanciarProdutoRepository();

            var retorno = await appService.ObterProduto(produtoEntity.Id);

            retorno.Id.Should().Be(Guid.Parse(produtoEntity.Id));
            retorno.Nome.Should().Be(produtoEntity.Nome);
            retorno.Preco.Should().Be(produtoEntity.Preco);
            retorno.Descricao.Should().Be(produtoEntity.Descricao);
            retorno.Qtd.Should().Be(produtoEntity.QtdEstoque);

        }
      
        [Test]
        public void BuscaProdutoERetornaNull()
        {
            var produtoEntity = _fixture.Build<ProdutoEntity>()
                .With(produto => produto.Id, Guid.Empty.ToString())
                .Create();

            _baseRepository
             .Setup(mock => mock.BuscarEntidade<ProdutoEntity>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            var appService = InstanciarProdutoRepository();

            var retorno = appService.ObterProduto(produtoEntity.Id);

            retorno.Result.Should().BeNull();

        }

        [Test]
        public void BuscaTodosOsProdutosERetornaListaDeProdutos()
        {
            var protudoEntity = _fixture.Build<ProdutoEntity>()
                .With(produto => produto.Id, Guid.Empty.ToString())
                .Create();

            var produtosEntities = new List<ProdutoEntity>();
            produtosEntities.Add(protudoEntity);

            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesAsync<ProdutoEntity>(It.IsAny<string>()))
             .ReturnsAsync(produtosEntities);

            var appService = InstanciarProdutoRepository();

            var retorno = appService.ObterTodosProdutos();

            retorno.Result[0].Id.Should().Be(Guid.Parse(produtosEntities[0].Id));
            retorno.Result[0].Nome.Should().Be(produtosEntities[0].Nome);
            retorno.Result[0].Preco.Should().Be(produtosEntities[0].Preco);
            retorno.Result[0].Descricao.Should().Be(produtosEntities[0].Descricao);
            retorno.Result[0].Qtd.Should().Be(produtosEntities[0].QtdEstoque);
        }

        [Test]
        public void BuscaTodosOsProdutosERetornaNull()
        {
            _baseRepository
             .Setup(mock => mock.BuscarTodasEntidadesAsync<ProdutoEntity>(It.IsAny<string>()))
             .ReturnsAsync(new List<ProdutoEntity>());

            var appService = InstanciarProdutoRepository();

            var retorno = appService.ObterTodosProdutos();

            retorno.Result.Should().BeNull();           
        }

        [Test]
        public async Task AtualizaProdutoERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();

            _baseRepository
             .Setup(mock => mock.AtualizarEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()))
             .ThrowsAsync(exception);

            var appService = InstanciarProdutoRepository();

            try
            {
                await appService.AtualizarProduto(_fixture.Create<Produto>());
            }
            catch (Exception ex)
            {
                ex.Should().BeEquivalentTo(exception);
            }

        }

        [Test]
        public async Task AtualizaListaDeProdutosERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();
            var produtos = _fixture.CreateMany<Produto>().ToList();

            _baseRepository
               .Setup(mock => mock.AtualizarEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()))
               .ThrowsAsync(exception)
               .Verifiable();

            var appService = InstanciarProdutoRepository();
            try
            {
                await appService.AtualizarProdutos(produtos);
            }
            catch (Exception ex)
            {
                ex.Should().BeEquivalentTo(exception);
            }

        }

        [Test]
        public async Task InsereProdutosERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();
            var retornoExeption = new Exception();

            _baseRepository
              .Setup(mock => mock.InserirEntidade(It.IsAny<ProdutoEntity>(), It.IsAny<string>()))
              .ThrowsAsync(exception)
              .Verifiable();

            var appService = InstanciarProdutoRepository();

            try
            {
                await appService.InserirProduto(_fixture.Create<Produto>());
            }
            catch (Exception ex)
            {
                retornoExeption = ex;
            }

            retornoExeption.Should().BeEquivalentTo(exception); //Forma mais correta
        }

        [Test]
        public async Task BuscaProdutoERetornaExcecao()
        {
            var exception = _fixture.Create<Exception>();
            var retornoExeption = new Exception();
            var produtoEntity = _fixture.Build<ProdutoEntity>()
                           .With(produto => produto.Id, Guid.Empty.ToString())
                           .Create();

            _baseRepository
             .Setup(mock => mock.BuscarEntidade<ProdutoEntity>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
             .ThrowsAsync(exception);

            var appService = InstanciarProdutoRepository();

            try
            {
                var retorno = await appService.ObterProduto(produtoEntity.Id);
            }
            catch (Exception ex)
            {
                retornoExeption = ex;
            }

            retornoExeption.Should().BeEquivalentTo(exception);
        }

        private ProdutoRepository InstanciarProdutoRepository()
        {
            return new ProdutoRepository(
                  _baseRepository.Object
            );
        }
    }
}