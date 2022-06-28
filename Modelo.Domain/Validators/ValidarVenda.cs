using FluentValidation;
using Modelo.Domain.Models;
using System;

namespace Modelo.Domain.Validators
{
    public class ValidarVenda : AbstractValidator<Venda>
    {
        public ValidarVenda()
        {         
       
            RuleFor(c => c.ProdutoVendidos)
                .NotEmpty().WithMessage("Por favor escolha pelo menos um produto.")
                .NotNull().WithMessage("Por favor escolha pelo menos um produto.");

        }
    }
}
