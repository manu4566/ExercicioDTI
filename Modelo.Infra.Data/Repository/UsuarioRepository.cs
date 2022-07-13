using Microsoft.WindowsAzure.Storage.Table;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infra.Data.Entities;
using Modelo.Infra.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IBaseRepository _baseRepository;
        public UsuarioRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task InserirUsuario(Usuario usuario)
        {
            try
            {
                var usuarioEntity = ConverterUsuarioParaUsuarioEntity(usuario);

                await _baseRepository.InserirEntidade(usuarioEntity, typeof(UsuarioEntity).Name);
            }
            catch(Exception ex)
            {
                throw ex;
            }             
        }

        public async Task<bool> ConferirExistenciaDeCpfEEmail(string cpf, string email)
        {
            try
            {
                var query = new TableQuery<UsuarioEntity>().Where(
                   TableQuery.CombineFilters(
                   TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, cpf),
                   TableOperators.Or,
                   TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, email)));

                var entidades = await _baseRepository.BuscarEntidadesQueryAsync(query, typeof(UsuarioEntity).Name);

                return entidades.Any();
            }          
            catch (Exception ex)
            {
                throw ex;
            }
 
        }

        public async Task<Usuario> ObterUsuarioPeloCpf(string cpf)
        {
            try
            {
                var usuariosEntities = await _baseRepository.BuscarTodasEntidadesPartitionKeyAsync<UsuarioEntity>(cpf, typeof(UsuarioEntity).Name);
                //Como o CPF do usuário é unico, apesar de retornar uma lista, ela é de tamanho unitario ou nula, se não existir o usuario com esse email

                return ConverterUsuarioEntityParaUsuario(usuariosEntities.First<UsuarioEntity>());
            }            
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        public async Task<Usuario> ObterUsuarioPeloEmail(string email)
        {
            try 
            {
                var usuariosEntities = await _baseRepository.BuscarTodasEntidadesRowKeyAsync<UsuarioEntity>(email, typeof(UsuarioEntity).Name);
                //Como o email do usuário é unico, apesar de retornar uma lista, ela é de tamanho unitario ou nula, se não existir o usuario com esse cpf

                return ConverterUsuarioEntityParaUsuario(usuariosEntities.First<UsuarioEntity>());
            }            
            catch (Exception ex)
            {
                throw ex;
            }
           
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
