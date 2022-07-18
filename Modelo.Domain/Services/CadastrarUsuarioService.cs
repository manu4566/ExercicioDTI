using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using System;
using Modelo.Domain.Validators;
using Modelo.Share;

namespace Modelo.Domain.Services
{
    public class CadastrarUsuarioService : ICadastrarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public CadastrarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        } 
        public async Task<string> CadastrarUsuario(Usuario usuario)
        {
            try
            {
                string msgRetorno;

                if (CpfUteis.VerificarCpf(usuario.Cpf))
                {
                    var condicaoCpfEmail = await _usuarioRepository.ConferirExistenciaDeCpfEEmail(usuario.Cpf, usuario.Email);
                  
                    if (!condicaoCpfEmail)
                    {
                        usuario.Cpf = CpfUteis.PadronizarCpf(usuario.Cpf);

                        await _usuarioRepository.InserirUsuario(usuario);

                        msgRetorno = AppConstantes.Api.Sucesso.Cadastro;
                    }
                    else
                    {
                        msgRetorno = AppConstantes.Api.Erros.Usuario.DadosInvalidos;
                    }
                }
                else
                {
                    msgRetorno = AppConstantes.Api.Erros.Usuario.CpfInvalido;
                }

                return msgRetorno;

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
             
        }

        public async Task<Usuario> BuscarUsuario(string cpf)
        {
            try
            {  
                return await _usuarioRepository.ObterUsuarioPeloCpf(cpf);
            }
            catch(Exception ex)
            {
                throw ex;
            }       
            
        }

    }
}
