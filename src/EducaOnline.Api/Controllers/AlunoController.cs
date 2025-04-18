using MediatR;
using EducaOnline.Core.Communication;
using EducaOnline.Aluno.Application.Services;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Api.Controllers
{
    [Route("api/aluno")]
    public class AlunoController : MainController
    {
        private readonly IAlunoAppService _alunoAppService;

        public AlunoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IAlunoAppService alunoAppService) : base(notifications, mediatorHandler)
        {
            _alunoAppService = alunoAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
