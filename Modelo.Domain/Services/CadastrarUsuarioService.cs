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
        public async Task<bool> CadastrarUsuario(Usuario usuario)
        { 
            
            var condicaoCpfEmail = await _usuarioRepository.ConferirExistenciaDeCpfEEmail(usuario.Cpf,usuario.Email);
           
            var condicaoCPFValido = ValidarCPF.VerificarCpf(usuario.Cpf);

            if (!condicaoCpfEmail && condicaoCPFValido)
            {
                usuario.Cpf = ValidarCPF.PadronizarCpf(usuario.Cpf);

                var result = await _usuarioRepository.InserirUsuario(usuario);

                return result;
            }

            // Melhorar a Tratativa de erro???
            return false;      
        }

        public async Task<Usuario> BuscarUsuario(string cpf)
        {
            //Se o usuario não existir???? Como saber??? É aqui que acontece a tratativa de erro? 
           return await _usuarioRepository.ObterUsuarioPeloCpf(cpf);
            
        }

    }
}
