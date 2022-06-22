using Modelo.Infra.Data.Entities;
using System;


namespace Modelo.Infra.Data.Interface
{
    public interface IUsuarioRepository
    {
        public UsuarioEntity ObterUsuario(string cpf);
        public void InserirUsuario(UsuarioEntity usuarioEntity);
    }
}
