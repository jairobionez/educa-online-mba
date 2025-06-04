using EducaOnline.Aluno.Application.Events;
using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Enums;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Moq;

namespace EducaOnline.Aluno.Teste
{
    public class AlunoIntegrationEventHandlerTests
    {
        private Mock<IAlunoRepository> _alunoRepositoryMock;
        private Mock<INotificationHandler<DomainNotification>> _notificationHandlerMock;
        private AlunoIntegrationEventHandler _handler;

        [SetUp]
        public void Setup()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _notificationHandlerMock = new Mock<INotificationHandler<DomainNotification>>();
            _handler = new AlunoIntegrationEventHandler(_notificationHandlerMock.Object, _alunoRepositoryMock.Object);
        }

        [Test]
        public async Task Deve_Notificar_Se_Pagamento_Tiver_Falha()
        {
            var evento = new PagamentoMatriculaEvent(Guid.NewGuid(), Guid.NewGuid(), nameof(StatusPagamentoEnum.FALHA_PAGAMENTO));

            await _handler.Handle(evento, CancellationToken.None);

            _notificationHandlerMock.Verify(x =>
                x.Handle(It.Is<DomainNotification>(n => n.Value.Contains("Falha ao realizar pagamento")),
                         It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Deve_Notificar_Se_Aluno_Ja_Teve_Curso_Concluido()
        {
            var alunoId = Guid.NewGuid();
            var evento = new PagamentoMatriculaEvent(Guid.NewGuid(), alunoId, nameof(StatusPagamentoEnum.SUCESSO_PAGAMENTO));

            var alunoMock = new Domain.Aluno(alunoId, "Jairo Bionez", "jairo@devio.com");
            alunoMock.RealizarMatricula(Guid.NewGuid(), 5);
            alunoMock.AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);

            _alunoRepositoryMock.Setup(x => x.BuscarAlunoPorId(alunoId)).ReturnsAsync(alunoMock);

            await _handler.Handle(evento, CancellationToken.None);

            _notificationHandlerMock.Verify(x =>
                x.Handle(It.Is<DomainNotification>(n => n.Value.Contains("curso já concluído")),
                         It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Deve_Atualizar_Status_Se_Pagamento_Sucesso()
        {
            var alunoId = Guid.NewGuid();
            var evento = new PagamentoMatriculaEvent(Guid.NewGuid(), alunoId, nameof(StatusPagamentoEnum.SUCESSO_PAGAMENTO));

            var alunoMock = new Domain.Aluno(alunoId, "Jairo Bionez", "jairo@devio.com");
            alunoMock.RealizarMatricula(Guid.NewGuid(), 5);
            alunoMock.AtualizarStatusMatricula(StatusMatriculaEnum.PENDENTE_PAGAMENTO);


            _alunoRepositoryMock.Setup(x => x.BuscarAlunoPorId(alunoId)).ReturnsAsync(alunoMock);
            _alunoRepositoryMock.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);

            await _handler.Handle(evento, CancellationToken.None);

            _alunoRepositoryMock.Verify(x => x.AtualizarAluno(It.IsAny<Domain.Aluno>()), Times.Once);
        }
    }
}