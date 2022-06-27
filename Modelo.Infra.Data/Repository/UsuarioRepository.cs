
using Modelo.Domain.Models;
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

        public async Task<bool> InserirUsuario(Usuario usuario)
        {
            var usuarioEntity = ConverterUsuarioParaUsuarioEntity(usuario);

            var result = await _baseRepository.InserirEntidade(usuarioEntity, typeof(UsuarioEntity).Name);

            if (result.Result != null) return true;

            return false;
        }

        public async Task<Usuario> ObterUsuarioPeloCpf(string cpf)
        {
            var usuariosEntities = await _baseRepository.BuscarTodasEntidadesPartitionKeyAsync<UsuarioEntity>( cpf, typeof(UsuarioEntity).Name);
            //Como o CPF do usuário é unico, apesar de retornar uma lista, ela é de tamanho unitario ou nula, se não existir o usuario com esse email

            return ConverterUsuarioEntityParaUsuario(usuariosEntities.First<UsuarioEntity>());
        }

        public async Task<Usuario> ObterUsuarioPeloEmail(string email)
        {
            var usuariosEntities = await _baseRepository.BuscarTodasEntidadesRowKeyAsync<UsuarioEntity>(email, typeof(UsuarioEntity).Name);
            //Como o email do usuário é unico, apesar de retornar uma lista, ela é de tamanho unitario ou nula, se não existir o usuario com esse cpf

            return ConverterUsuarioEntityParaUsuario(usuariosEntities.First<UsuarioEntity>());
        }

        private UsuarioEntity ConverterUsuarioParaUsuarioEntity(Usuario usuario)
        {
            return new UsuarioEntity
            {
                PartitionKey = usuario.Cpf,
                RowKey = usuario.Email,

                CPF = usuario.Cpf,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Admin = usuario.Admin
            };
        }

        private Usuario ConverterUsuarioEntityParaUsuario(UsuarioEntity usuarioEntity)
        {
            return new Usuario
            {
                Cpf = usuarioEntity.CPF,
                Nome = usuarioEntity.Nome,
                Email = usuarioEntity.Email,
                Senha = usuarioEntity.Senha,
                Admin = usuarioEntity.Admin
            };
        }
    }
}
