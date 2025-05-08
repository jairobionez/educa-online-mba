using EducaOnline.Core.Communication;
using EducaOnline.Core.Enums;
using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.Financeiro.Domain;
using EducaOnline.Financeiro.Domain.ValueObjects;
using MediatR;

namespace EducaOnline.Financeiro.Application.Commands
{
    public class FinanceiroCommandHandler : IRequestHandler<RealizarPagamentoMatriculaCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFinanceiroRepository _financeiroRepository;

        public FinanceiroCommandHandler(IMediatorHandler mediatorHandler, IFinanceiroRepository financeiroRepository)
        {
            _mediatorHandler = mediatorHandler;
            _financeiroRepository = financeiroRepository;
        }

        public async Task<bool> Handle(RealizarPagamentoMatriculaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var cartaoCredito = new DadosCartao(request.NomeCartao, request.NumeroCartao, request.ExpiracaoCartao, request.CvvCartao);

            var pagamento = new Pagamento(request.CursoId, request.AlunoId, request.ValorCurso);
            pagamento.AdicionarDadosCartao(cartaoCredito);

            if (pagamento.CartaoValido())
                pagamento.AtualizarStatus((int)StatusPagamentoEnum.SUCESSO_PAGAMENTO, nameof(StatusPagamentoEnum.SUCESSO_PAGAMENTO));
            else
                pagamento.AtualizarStatus((int)StatusPagamentoEnum.FALHA_PAGAMENTO, nameof(StatusPagamentoEnum.FALHA_PAGAMENTO));

            _financeiroRepository.AdicionarPagamento(pagamento);
            await _financeiroRepository.UnitOfWork.Commit();

            await _mediatorHandler.PublicarEvento(new PagamentoMatriculaEvent(request.CursoId, request.AlunoId, pagamento.StatusPagamento.Descricao));

            return true;
        }


        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
