using MediatR;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace EducaOnline.Api.Controllers
{
    [Route("api/aluno")]
    public class AlunoController : MainController
    {

        public AlunoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
