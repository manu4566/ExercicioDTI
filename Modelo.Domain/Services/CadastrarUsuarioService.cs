using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using System;
using Modelo.Domain.Validators;

namespace Modelo.Domain.Services
{
    public class CadastrarUsuarioService : ICadastrarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public CadastrarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        } 
        public async Task CadastrarUsuario(Usuario usuario)
        {
            try
            {
                if (CpfUteis.VerificarCpf(usuario.Cpf))
                {
                    var condicaoCpfEmail = await _usuarioRepository.ConferirExistenciaDeCpfEEmail(usuario.Cpf, usuario.Email);
                  
                    if (!condicaoCpfEmail)
                    {
                        usuario.Cpf = CpfUteis.PadronizarCpf(usuario.Cpf);

                        await _usuarioRepository.InserirUsuario(usuario);

                    }
                    else
                    {
                        usuario.Erro = "Erro: CPF ou Email já cadastrados.";
                    }
                }
                else
                {
                    usuario.Erro = "Erro: Cpf não é valido.";
                }

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
