using Modelo.Application.DTO;
using Modelo.Application.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Application.Mapping
{
    public class ConverterUsuario : IConverterUsuario
    {
        public Usuario UsuarioDtoParaUsuario(UsuarioDto usuarioDto)
        {
            return new Usuario
            {
                Admin = usuarioDto.Admin,
                Cpf = usuarioDto.Cpf,
                Senha = usuarioDto.Senha,
                Email = usuarioDto.Email,
                Nome = usuarioDto.Nome
            };
        }

        public UsuarioDto UsuarioParaUsuarioDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Admin = usuario.Admin,
                Cpf = usuario.Cpf,
                Senha = usuario.Senha,
                Email = usuario.Email,
                Nome = usuario.Nome
            };
        }
    }
}
