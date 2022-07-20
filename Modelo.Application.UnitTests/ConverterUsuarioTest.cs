using AutoFixture;
using Modelo.Application.DTO;
using Modelo.Application.Enum;
using Modelo.Application.Mapping;
using Modelo.Domain.Models;
using Modelo.Share;
using Moq;
using FluentAssertions;

namespace Modelo.Application.UnitTests
{
    public class ConverterUsuarioTest
    {
        private IFixture _fixture;
     
       [SetUp] 
        public void Setup()
        {
            _fixture = new Fixture();           
        }

        [Test] 
        public void DeveConverterUsuarioParaUsuarioDto()
        {
            var usuario = _fixture.Create<Usuario>();
          
            var converter = new ConverterUsuario();
            var retorno = converter.UsuarioParaUsuarioDto(usuario);

            retorno.Cpf.Should().Be(usuario.Cpf);
            retorno.Nome.Should().Be(usuario.Nome);
            retorno.Email.Should().Be(usuario.Email);
            retorno.Senha.Should().Be(usuario.Senha);
            retorno.Admin.Should().Be(usuario.Admin);
        }

        [Test]
        public void DeveConverterUsuarioDtoParaUsuario()
        {
            var usuarioDto = _fixture.Create<UsuarioDto>();       
            var converter = new ConverterUsuario();

            var retorno = converter.UsuarioDtoParaUsuario(usuarioDto);

            retorno.Cpf.Should().Be(usuarioDto.Cpf);
            retorno.Nome.Should().Be(usuarioDto.Nome);
            retorno.Email.Should().Be(usuarioDto.Email);
            retorno.Senha.Should().Be(usuarioDto.Senha);
            retorno.Admin.Should().Be(usuarioDto.Admin);
        }       
     
    }
}