using Modelo.Application.DTO;
using Modelo.Domain.Models;


namespace Modelo.Application.Interfaces
{
    public interface IConverterUsuario
    {
        public Usuario UsuarioDto_Usuario(UsuarioDto usuarioDto);
        public UsuarioDto Usuario_UsuarioDto(Usuario usuario);

    }
}
