using Modelo.Application.DTO;
using Modelo.Domain.Models;


namespace Modelo.Application.Interfaces
{
    public interface IConverterUsuario
    {
        public Usuario UsuarioDtoParaUsuario(UsuarioDto usuarioDto);
        public UsuarioDto UsuarioParaUsuarioDto(Usuario usuario);

    }
}
