using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> CadastrarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuario(string cpf);

    }
}
