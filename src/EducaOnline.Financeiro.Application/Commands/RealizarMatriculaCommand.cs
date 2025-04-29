using EducaOnline.Core.Messages;
using FluentValidation;

namespace EducaOnline.Financeiro.Application.Commands
{
    public class RealizarMatriculaCommand : Command
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
        public decimal ValorCurso { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new RealizarMatriculaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }


    public class RealizarMatriculaValidation : AbstractValidator<RealizarMatriculaCommand>
    {
        public RealizarMatriculaValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do aluno inválido");

            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Curso não informado.");

            RuleFor(c => c.NomeCartao)
                .NotEqual(string.Empty)
                .WithMessage("Nome do cartão inválido.");

            RuleFor(c => c.NumeroCartao)
              .NotEqual(string.Empty)
              .WithMessage("Numero do cartão inválido")
              .MinimumLength(16)
              .WithMessage("Numero do cartão inválido");

            RuleFor(c => c.ExpiracaoCartao)
              .NotNull()
              .WithMessage("Data de expiração não informada.")
              .GreaterThan(DateTime.Now)
              .WithMessage("Data de expiração do cartão inválida");
             

            RuleFor(c => c.CvvCartao)
              .NotEqual(string.Empty)
              .WithMessage("Ccv inválido")
              .MinimumLength(3)
              .WithMessage("Ccv inválido");
        }
    }
}
