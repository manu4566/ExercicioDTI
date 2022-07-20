using AutoFixture;
using FluentAssertions;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Domain.Services;
using Modelo.Share;
using Moq;

namespace Modelo.Domain.UnitTests
{
    public class VendaServiceTest
    {
        private IFixture _fixture;

        private Mock<IProdutoRepository> _produtoRepository;
        private Mock<IVendaRepository> _vendasRepository;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _produtoRepository = new Mock<IProdutoRepository>();
            _vendasRepository = new Mock<IVendaRepository>();
        }

        [Test]
        public void CadastraVendaQuandoHaProdutosDisponiveisNoEstoqueERetornaMensagemDeSucesso()
        {
            var produto = _fixture.Build<Produto>()
                .With(produto => produto.Id, Guid.Empty)
                .With(produto => produto.Qtd, 10)                
                .Create();         

            var produtoVendido = _fixture.Build<ProdutoVendido>()
                .With(produto => produto.Id, Guid.Empty)
                .With(produto => produto.QtdVendida, 10)
                .Create();

            var produtosEntity = new List<Produto>();
            produtosEntity.Add(produto);

            var produtosVendidos = new List<ProdutoVendido>();
            produtosVendidos.Add(produtoVendido);

            var venda = _fixture.Build<Venda>()
                .With(venda => venda.ProdutosVendidos, produtosVendidos)
                .Create();

            _produtoRepository
              .Setup(mock => mock.ObterTodosProdutos())
              .ReturnsAsync(produtosEntity);  

            _produtoRepository
              .Setup(mock => mock.AtualizarProdutos(It.IsAny<List<Produto>>()))
              .Verifiable();

            _vendasRepository
              .Setup(mock => mock.InserirVenda(It.IsAny<Venda>()))
              .Verifiable();

            var appService = InstanciarVendaService();
         
            var retorno = appService.CadastrarVenda(venda);

            retorno.Result.Should().Be(AppConstantes.Api.Sucesso.Cadastro);
            _vendasRepository.Verify(mock => mock.InserirVenda(It.IsAny<Venda>()), Times.Once);
            _produtoRepository.Verify(mock => mock.AtualizarProdutos(It.IsAny<List<Produto>>()), Times.Once);
        }

        [Test]
        public void CadastraVendaQuandoNaoHaProdutosDisponiveisNoEstoqueERetornaMensagemDeEstoqueInsuficiente()
        {
            var produto = _fixture.Build<Produto>()
             .With(produto => produto.Id, Guid.Empty)
             .With(produto => produto.Qtd, 10)
             .Create();

            var produtoVendido = _fixture.Build<ProdutoVendido>()
                .With(produto => produto.Id, Guid.Empty)
                .With(produto => produto.QtdVendida, 15)
                .Create();

            var produtosEntity = new List<Produto>();
            produtosEntity.Add(produto);

            var produtosVendidos = new List<ProdutoVendido>();
            produtosVendidos.Add(produtoVendido);

            var venda = _fixture.Build<Venda>()
                .With(venda => venda.ProdutosVendidos, produtosVendidos)
                .Create();

            _produtoRepository
              .Setup(mock => mock.ObterTodosProdutos())
              .ReturnsAsync(produtosEntity)
              .Verifiable();        

            var appService = InstanciarVendaService();

            var retorno = appService.CadastrarVenda(venda);

            retorno.Result.Should().Be(AppConstantes.Api.Erros.Venda.EstoqueInsuficiente);
            _produtoRepository.Verify(mock => mock.ObterTodosProdutos(), Times.Once);
        }

        [Test]
        public void BuscaTodasAsVendasAssociadasAoCpfDoClienteERetornaListaDeVendas()
        {
            var vendas = _fixture.CreateMany<Venda>().ToList();
            
            _vendasRepository
              .Setup(mock => mock.ObterTodasVendas(It.IsAny<string>()))
              .ReturnsAsync(vendas);

            var appService = InstanciarVendaService();

            var retorno = appService.ObterTodasVendas(It.IsAny<string>());

            Assert.AreEqual(retorno.Result, vendas);
        }

        [Test]
        public void BuscaDetalhesDaVendaComIdNaoRegistradoERetornaNull()
        {
            _vendasRepository
              .Setup(mock => mock.ObterVenda(It.IsAny<string>()));

            var appService = InstanciarVendaService();

            var retorno = appService.ObterDetalhesDaVenda(It.IsAny<Guid>());

            retorno.Result.Should().BeNull();
        }

        [Test]
        public void BuscaDetalhesDaVendaComIdRegistradoERetornaDetalhesDaVenda()
        {
            var produto = _fixture.Build<Produto>()
           .With(produto => produto.Id, Guid.Empty)
           .With(produto => produto.Nome, "nome")
           .With(produto => produto.Descricao, "descricao")
           .With(produto => produto.Qtd, 10)
           .With(produto => produto.Preco, 10)
           .Create();

            var produtoVendido = _fixture.Build<ProdutoVendido>()
                .With(produto => produto.Id, Guid.Empty)
                .With(produto => produto.QtdVendida, 1)                
                .Create();


            var detalheProdutoVendido = _fixture.Build<Produto>()
           .With(produto => produto.Id, Guid.Empty)
           .With(produto => produto.Nome, "nome")
           .With(produto => produto.Descricao, "descricao")
           .With(produto => produto.Qtd, 1)
           .With(produto => produto.Preco, 10)
           .Create();

            new Produto
            {
                Descricao = "descricao",
                Nome = "nome",
                Preco = 10,
                Id = Guid.Empty,
                Qtd = 1
            };

            var valorTotal = detalheProdutoVendido.Qtd * detalheProdutoVendido.Preco;

            var produtosEntity = new List<Produto>();
            produtosEntity.Add(produto);

            var produtosVendidos = new List<ProdutoVendido>();
            produtosVendidos.Add(produtoVendido);

            var detalhesProdutosVendidos = new List<Produto>();
            detalhesProdutosVendidos.Add(detalheProdutoVendido);

            var venda = _fixture.Build<Venda>()
                .With(venda => venda.ProdutosVendidos, produtosVendidos)
                .Create();

            _vendasRepository
             .Setup(mock => mock.ObterVenda(It.IsAny<string>()))
             .ReturnsAsync(venda);

            _produtoRepository
              .Setup(mock => mock.ObterTodosProdutos())
              .ReturnsAsync(produtosEntity);
           

            var appService = InstanciarVendaService();

            var retorno = appService.ObterDetalhesDaVenda(It.IsAny<Guid>());

            retorno.Result.Id.Should().Be(venda.Id);
            retorno.Result.Cpf.Should().Be(venda.Cpf);
            retorno.Result.ValorTotal.Should().Be(valorTotal);

            retorno.Result.ProdutosVendidos[0].Id.Should().Be(detalhesProdutosVendidos[0].Id);
            retorno.Result.ProdutosVendidos[0].Descricao.Should().Be(detalhesProdutosVendidos[0].Descricao);
            retorno.Result.ProdutosVendidos[0].Qtd.Should().Be(detalhesProdutosVendidos[0].Qtd);
            retorno.Result.ProdutosVendidos[0].Preco.Should().Be(detalhesProdutosVendidos[0].Preco);
            retorno.Result.ProdutosVendidos[0].Nome.Should().Be(detalhesProdutosVendidos[0].Nome);

        }

        private VendaService InstanciarVendaService()
        {
            return new VendaService(
                    _vendasRepository.Object,
                    _produtoRepository.Object
                   
            );
        }
    }
}
