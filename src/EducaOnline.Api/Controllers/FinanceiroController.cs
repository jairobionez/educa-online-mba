using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.Financeiro.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Api.Controllers
{
    [Route("api/financeiro")]
    public class FinanceiroController : MainController
    {

        public FinanceiroController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
        }

        [HttpPost("pagamento")]
        public async Task<IActionResult> RealizarPagamento([FromBody] RealizarPagamentoMatriculaCommand command)
        {
            await _mediatorHandler.EnviarComando(command);

            if(OperacaoValida())
                return Ok("Pagamento efetuado com sucesso!");

            return BadRequest(ObterMensagensErro());
        }
    }
}
