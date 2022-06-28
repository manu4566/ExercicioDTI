using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface ICadastrarUsuarioService
    {
        Task<bool> CadastrarUsuario(Usuario usuario);
        Task<Usuario> BuscarUsuario(string cpf);

    }
}
