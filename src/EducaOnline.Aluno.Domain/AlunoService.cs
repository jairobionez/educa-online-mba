using EducaOnline.Core.Helpers;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace EducaOnline.Aluno.Domain
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;
        private readonly INotificationHandler<DomainNotification> _domainNotification;

        public AlunoService(IAlunoRepository repository, INotificationHandler<DomainNotification> domainNotification)
        {
            _repository = repository;
            _domainNotification = domainNotification;
        }

        public async Task AdicionarAluno(Aluno aluno)
        {

            var alunoDb = await _repository.BuscarAlunoPorRa(aluno.Ra);

            if (aluno is not null)
            {
              
            }

            if (EmailValidator.IsValid(aluno.Email))
            {

            }
        }


        public async Task<Domain.Aluno> BuscarAlunoPorId(Guid id)
        {
            return await _repository.BuscarAlunoPorId(id);
        }

        public async Task<Domain.Aluno> BuscarAlunoPorRa(string ra)
        {
            return await _repository.BuscarAlunoPorRa(ra);
        }
    }
}
