using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Enums;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace EducaOnline.Aluno.Application.Events
{
    public class AlunoIntegrationEventHandler : INotificationHandler<PagamentoMatriculaEvent>
    {

        private readonly IAlunoRepository _alunoRepository;
        private readonly INotificationHandler<DomainNotification> _domainNotification;

        public AlunoIntegrationEventHandler(INotificationHandler<DomainNotification> domainNotification, IAlunoRepository alunoRepository)
        {
            _domainNotification = domainNotification;
            _alunoRepository = alunoRepository;
        }

        public async Task Handle(PagamentoMatriculaEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Status.Equals(nameof(StatusPagamentoEnum.FALHA_PAGAMENTO)))
            {
                await _domainNotification.Handle(new DomainNotification("Pagamento", $"Falha ao realizar pagamento, dados do cartão inválidos | Código {nameof(StatusPagamentoEnum.FALHA_PAGAMENTO)}"), new CancellationToken());
                return;
            }

            var aluno = await _alunoRepository.BuscarAlunoPorId(notification.AlunoId);
            aluno.AtualizarStatusMatricula(StatusMatriculaEnum.EM_ANDAMENTO);

            _alunoRepository.AtualizarAluno(aluno);
            await _alunoRepository.UnitOfWork.Commit();
        }
    }
}
