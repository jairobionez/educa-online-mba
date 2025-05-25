using EducaOnline.Aluno.AntiCorruption;
using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Communication;
using EducaOnline.Core.DomainObjects;
using EducaOnline.Core.Enums;
using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace EducaOnline.Aluno.Application.Commands
{
    public class AlunoCommandHandler :
        IRequestHandler<AdicionarAlunoCommand, bool>,
        IRequestHandler<RealizarMatriculaCommand, bool>,
        IRequestHandler<ConcluirAulaCommand, bool>,
        IRequestHandler<EmitirCertificadoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IAulaService _aulaService;

        public AlunoCommandHandler(IMediatorHandler mediatorHandler, IAlunoRepository alunoRepository, IAulaService aulaService)
        {
            _mediatorHandler = mediatorHandler;
            _alunoRepository = alunoRepository;
            _aulaService = aulaService;
        }

        public async Task<bool> Handle(AdicionarAlunoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var aluno = new Domain.Aluno(request.Id, request.Nome, request.Email);

            var ra = _alunoRepository.BuscarAlunos().Count() == 0 ? 10000 : (await _alunoRepository.BuscarUltimoRa()) + 1;
            aluno.VincularRa(ra);

            _alunoRepository.AdicionarAluno(aluno);
            await _alunoRepository.UnitOfWork.Commit();

            return true;
        }

        public async Task<bool> Handle(RealizarMatriculaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var aluno = await _alunoRepository.BuscarAlunoPorId(request.AlunoId);

            if (aluno == null)
                throw new DomainException("Aluno não encontrado.");

            if (aluno.Matricula != null)
                throw new DomainException("Aluno já está com uma matricula ativa");

            var totalAulas = await _aulaService.ObterTotalAulasPorCurso(request.CursoId);
            aluno.RealizarMatricula(request.CursoId, totalAulas);

            _alunoRepository.AtualizarAluno(aluno);
            await _alunoRepository.UnitOfWork.Commit();

            return true;
        }

        public async Task<bool> Handle(ConcluirAulaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var aluno = await _alunoRepository.BuscarAlunoPorId(request.AlunoId);

            if (aluno == null)
                throw new DomainException("Aluno não encontrado.");

            if (aluno.Matricula.Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO)
                throw new DomainException("Não é possível concluir a aula sem efetuar o pagamento.");

            if (aluno.Matricula.Status == StatusMatriculaEnum.CURSO_CONCLUIDO)
                throw new DomainException("Não é possível concluir após a conclusão do curso");

            var horasAula = await _aulaService.ObterHorasAulasPorCurso(request.CursoId, request.AulaId);

            aluno.ConcluirAula(request.AulaId, horasAula);

            _alunoRepository.AtualizarAluno(aluno);
            await _alunoRepository.UnitOfWork.Commit();

            return true;
        }

        public async Task<bool> Handle(EmitirCertificadoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.BuscarAlunoPorId(request.AlunoId);

            if (aluno.HistoricoAprendizado.Progresso >= 100)
            {
                var curso = await _aulaService.ObterNomeCurso(request.CursoId);

                aluno.EmitirCertificado(new Certificado(curso));
                aluno.AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);
            }

            _alunoRepository.AtualizarAluno(aluno);
            await _alunoRepository.UnitOfWork.Commit();

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
