using MediatR;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EducaOnline.Core.Enums;
using EducaOnline.Aluno.Application.Queries;
using EducaOnline.Financeiro.Application.Commands;
using EducaOnline.Aluno.Application.Commands;
using EducaOnline.Financeiro.Application;

namespace EducaOnline.Api.Controllers
{
    [Authorize]
    [Route("api/aluno")]
    public class AlunoController : MainController
    {
        private readonly IAlunoQueries _alunoQueries;

        public AlunoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IAlunoQueries alunoQueries) : base(notifications, mediatorHandler)
        {
            _alunoQueries = alunoQueries;
        }

        [HttpGet]
        public IActionResult BuscarDadosAluno()
        {
            return Ok(_alunoQueries.BuscarAlunos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarDadosAluno(Guid id)
        {
            return Ok(await _alunoQueries.BuscarAluno(id));
        }

        [HttpPost("{id}/curso/{cursoId}/emitir-certificado")]
        public async Task<IActionResult> EmitirCertificado(Guid id, Guid cursoId)
        {
            var command = new EmitirCertificadoCommand(cursoId, id);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return Ok("Certifcado emitido com sucesso! Consulte seu perfil para vistualiza-lo!");

            return BadRequest(ObterMensagensErro());
        }


        [HttpPost("{id}/curso/{cursoId}/matricular")]
        public async Task<IActionResult> RealizarMatricula(Guid id, Guid cursoId)
        {
            await _mediatorHandler.EnviarComando(new RealizarMatriculaCommand(cursoId, id));

            if (OperacaoValida())
                return Ok("Matricula realizada com sucesso!");

            return BadRequest(ObterMensagensErro());
        }

        [HttpPost("{id}/curso/{cursoId}/pagamento-matricula")]
        public async Task<IActionResult> RealizarPagamento(Guid id, Guid cursoId, PagamentoViewModel model)
        {
            var command = new RealizarPagamentoMatriculaCommand(id, cursoId, model.NomeCartao, model.NumeroCartao, model.ExpiracaoCartao, model.CvvCartao, model.ValorCurso);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return Ok("Pagamento efetuado com sucesso!");

            return BadRequest(ObterMensagensErro());
        }

        [HttpPost("{id}/curso/{cursoId}/aula/{aulaId}/concluir")]
        public async Task<IActionResult> ConcluirAula(Guid id, Guid cursoId, Guid aulaId)
        {
            var command = new ConcluirAulaCommand(id, cursoId, aulaId);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
                return Ok("Aula concluída com sucesso!");

            return BadRequest(ObterMensagensErro());
        }

    }
}
