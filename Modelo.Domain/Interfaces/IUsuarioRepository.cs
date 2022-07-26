using Modelo.Domain.Models;
using System;

namespace Modelo.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<Usuario> ObterUsuarioPeloCpf(string cpf);      
        public Task InserirUsuario(Usuario usuario);
        public Task<bool> ConferirExistenciaDeCpfEEmail( string cpf, string email);
    }
}
