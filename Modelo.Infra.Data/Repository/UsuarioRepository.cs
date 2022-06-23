
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Infra.Data.Repository
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly IBaseRepository _baseRepository;
        public UsuarioRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public void InserirUsuario(UsuarioEntity usuarioEntity)
        {
          //  _baseRepository.Insert( usuarioEntity, typeof(UsuarioEntity).Name);
            
        }

        public UsuarioEntity ObterUsuario(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
