using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using System;


namespace Modelo.Infra.Data.Interface
{
    public interface IUsuarioRepository
    {
        public Task<Usuario> ObterUsuarioPeloCpf(string cpf);
        public Task<Usuario> ObterUsuarioPeloEmail(string email);
        public bool InserirUsuario(Usuario usuario);
    }
}
