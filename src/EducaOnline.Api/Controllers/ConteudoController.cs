using EducaOnline.Conteudo.Application.Services;
using EducaOnline.Conteudo.Application.ViewModels;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Api.Controllers
{

    [Route("api/curso")]
    public class ConteudoController : MainController
    {
        private readonly IConteudoAppService _conteudoAppService;

        public ConteudoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IConteudoAppService conteudoAppService) : base(notifications, mediatorHandler)
        {
            _conteudoAppService = conteudoAppService;
        }

        /// <summary>
        /// Lista os cursos
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [ProducesResponseType(typeof(List<CursoResponseViewModel>), 200)]
        //[ProducesResponseType(typeof(InternalServerErrorModel), 500)]
        //[ProducesResponseType(typeof(BadRequestModel), 400)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _conteudoAppService.BuscarCursos());
        }

        /// <summary>
        /// Busca um curso especifico por id
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [ProducesResponseType(typeof(CursoResponseViewModel), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _conteudoAppService.BuscarCurso(id));
        }

        /// <summary>
        /// Adicionar um novo curso
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpPost]
        public async Task<IActionResult> AdicionarCurso(CursoViewModel model)
        {
            await _conteudoAppService.AdicionarCurso(model);
            return Ok();
        }

        /// <summary>
        /// Altera o nome do curso
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarNomeCurso(Guid id, CursoNomeViewModel model)
        {
            await _conteudoAppService.AlterarNomeCurso(id, model);
            return Ok();
        }

        /// <summary>
        /// Alterar o conteudo programtico do curso
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpPut("{id}/conteudo-programatico")]
        public async Task<IActionResult> AlterarNomeCurso(Guid id, ConteudoProgramaticoViewModel model)
        {
            await _conteudoAppService.AlterarConteudoProgramticoCurso(id, model);
            return Ok();
        }

        /// <summary>
        /// Desatia um curso
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpPut("{id}/desativar")]
        public async Task<IActionResult> DesativarCurso(Guid id)
        {
            await _conteudoAppService.DesativarCurso(id);
            return Ok();
        }

        /// <summary>
        /// Adiciona uma aula ao curso informado
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpPost("{id}/aula")]
        [ProducesResponseType(typeof(List<CursoResponseViewModel>), 200)]
        public async Task<IActionResult> AdicionarAula(Guid id, AulaViewModel aula)
        {
            return Ok(await _conteudoAppService.AdicionarAula(id, aula));
        }

        /// <summary>
        /// Altera uma aula do curso informado
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpPut("{id}/aula/{aulaId}")]
        [ProducesResponseType(typeof(List<CursoResponseViewModel>), 200)]
        public async Task<IActionResult> AlterarAula(Guid id, Guid aulaId, AulaViewModel aula)
        {
            return Ok(await _conteudoAppService.AlterarAula(id, aulaId, aula));
        }

        /// <summary>
        /// Remove uma aula do curso informado
        /// </summary>
        /// <response code="401">Token não autorizado. Favor contate o suporte</response>
        [HttpDelete("{id}/aula/{aulaId}")]
        [ProducesResponseType(typeof(List<CursoResponseViewModel>), 200)]
        public async Task<IActionResult> RemoverAula(Guid id, Guid aulaId)
        {
            return Ok(await _conteudoAppService.RemoverAula(id, aulaId));
        }
    }
}
