using FluentValidation;
using Modelo.Domain.Models;
using System;


namespace Modelo.Domain.Entities
{
    public class ValidarProduto : AbstractValidator<Produto>
    {
        public ValidarProduto()      
        {

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor entre com o Nome.")
                .NotNull().WithMessage("Por favor entre com o Nome.");

            RuleFor(c => c.Preco)
               .NotEmpty().WithMessage("Por favor entre com o Preco.")
               .NotNull().WithMessage("Por favor entre com o Preco.");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Por favor entre com a Descricao.")
                .NotNull().WithMessage("Por favor entre com a Descricao.");
           
            RuleFor(c => c.QtdEstoque)
               .NotEmpty().WithMessage("Por favor entre com a quantidade em estoque.")
               .NotNull().WithMessage("Por favor entre com a quantidade em estoque.");

        }
    }
}
