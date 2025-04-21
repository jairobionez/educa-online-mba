using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace EducaOnline.Aluno.Application.Events
{
    public class AlunoIntegrationEventHandler : INotificationHandler<PagamentoMatriculaEvent>
    {

        private readonly IAlunoService _alunoService;

        public AlunoIntegrationEventHandler(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public async Task Handle(PagamentoMatriculaEvent notification, CancellationToken cancellationToken)
        {
            // Atualizar o status da matricula de acordo com o status de pagamento

            throw new NotImplementedException();
        }
    }
}
