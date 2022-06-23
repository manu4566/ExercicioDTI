using FluentValidation;
using Modelo.Domain.Models;
using System;


namespace Modelo.Domain.Entities
{
    public class ValidarUsuario : AbstractValidator<Usuario>
    {
        public ValidarUsuario()
        {
            RuleFor(c => c.Cpf)
               .NotEmpty().WithMessage("Por favor entre com o CPF.")
               .NotNull().WithMessage("Por favor entre com o CPF.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor entre com o Nome.")
                .NotNull().WithMessage("Por favor entre com o Nome.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Por favor entre com o Email.")
                .NotNull().WithMessage("Por favor entre com o Email.");
           
            RuleFor(c => c.Senha)
               .NotEmpty().WithMessage("Por favor entre com a Senha.")
               .NotNull().WithMessage("Por favor entre com a Senha.");

            RuleFor(c => c.Admin)
                .NotEmpty().WithMessage("Por favor confirme se é administrador ou não.")
                .NotNull().WithMessage("Por favor confirme se é administrador ou não.");

          
        }
    }
}
