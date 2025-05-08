using EducaOnline.Core.Messages;
using FluentValidation;

namespace EducaOnline.Aluno.Application.Commands
{
    public class ConcluirAulaCommand : Command
    {
        public Guid AlunoId { get; set; }
        public Guid AulaId { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ConcluirAulaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ConcluirAulaValidation : AbstractValidator<ConcluirAulaCommand>
    {
        public ConcluirAulaValidation()
        {
            RuleFor(c => c.AulaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da aula inválido");

            RuleFor(c => c.AlunoId)
                   .NotEqual(Guid.Empty)
                   .WithMessage("Id do aluno inválido");
        }
    }
}
