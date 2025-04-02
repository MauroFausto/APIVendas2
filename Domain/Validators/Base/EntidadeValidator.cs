using FluentValidation;
using Domain.Models.Base;

namespace Domain.Validators.Base
{
    public class EntidadeValidator : AbstractValidator<Entidade>
    {
        public EntidadeValidator()
        {
            RuleFor(e => e.Id)
                .Must(e => e.GetType().Equals(typeof(Guid)))
                    .WithMessage("O campo Id deve ser um Guid.")
                .NotNull()
                    .WithMessage("O campo Id é obrigatório.");

            RuleFor(e => e.Observacoes)
                .Length(0, 500)
                    .WithMessage("O campo Observacoes deve ter no máximo 500 caracteres.");

            RuleFor(e => e.DataCriacao)
                .NotNull()
                    .WithMessage("O campo DataCriacao é obrigatório.")
                .Custom((dataCriacao, context) =>
                {
                    if (dataCriacao > DateTime.Now)
                        context.AddFailure("DataCriacao", "A Data de Criação não pode ser maior que a data atual.");
                });

            RuleFor(e => e.IdUsuarioCriacao)
                .NotNull()
                    .WithMessage("O campo IdUsuarioCriacao é obrigatório.")
                .NotEmpty()
                    .WithErrorCode("O campo IdUsuarioCriacao não pode estar vazio.");

            RuleFor(e => e.DataAtualizacao)
                .NotNull()
                    .WithMessage("O campo DataAtualizacao é obrigatório.")
                .Custom((dataAtualizacao, context) =>
                {
                    if (dataAtualizacao > DateTime.Now)
                        context.AddFailure("DataAtualizacao", "A Data de Atualização não pode ser maior que a data atual.");
                });

            RuleFor(e => e.IdUsuarioAtualizacao)
                .NotNull()
                    .WithMessage("O campo IdUsuarioAtualizacao é obrigatório.")
                .NotEmpty()
                    .WithMessage("O campo IdUsuarioAtualizacao não pode estar vazio.");
        }
    }
}
