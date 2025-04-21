using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Application.Commands
{
    public class AlunoCommandHandler : IRequestHandler<AdicionarAlunoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AlunoCommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarAlunoCommand request, CancellationToken cancellationToken)
        {
            // TODO: criar regra do aluno

            if (!ValidarComando(request)) 
                return false;

            throw new NotImplementedException();
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
