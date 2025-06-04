using EducaOnline.Core.Communication;
using EducaOnline.Core.Enums;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.Financeiro.Application.Commands;
using EducaOnline.Financeiro.Domain;
using Moq;

namespace EducaOnline.Conteudo.Teste
{
    public class FinanceiroCommandHandlerTests
    {

        private Mock<IMediatorHandler> _mediatorHandlerMock;
        private Mock<IFinanceiroRepository> _financeiroRepositoryMock;
        private FinanceiroCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _financeiroRepositoryMock = new Mock<IFinanceiroRepository>();

            _financeiroRepositoryMock.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);

            _handler = new FinanceiroCommandHandler(_mediatorHandlerMock.Object, _financeiroRepositoryMock.Object);
        }

        [Test]
        public async Task Deve_Realizar_Pagamento_Com_Cartao_Valido()
        {
            var command = new RealizarPagamentoMatriculaCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Cartão teste",
                "1111111111111111",
                DateTime.Now.AddYears(10),
                "123",
                20
            );

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            _financeiroRepositoryMock.Verify(x => x.AdicionarPagamento(It.IsAny<Pagamento>()), Times.Once);
            _mediatorHandlerMock.Verify(x => x.PublicarEvento(It.IsAny<PagamentoMatriculaEvent>()), Times.Once);
        }

        [Test]
        public async Task Deve_Realizar_Pagamento_Com_Cartao_Invalido()
        {
            var command = new RealizarPagamentoMatriculaCommand(
                 Guid.NewGuid(),
                 Guid.NewGuid(),
                 "Cartão teste",
                 "1111111111111110",
                 DateTime.Now.AddYears(10),
                 "123",
                 20
             );

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            _financeiroRepositoryMock.Verify(x => x.AdicionarPagamento(It.IsAny<Pagamento>()), Times.Once);
            _mediatorHandlerMock.Verify(x => x.PublicarEvento(It.Is<PagamentoMatriculaEvent>(
                e => e.Status == nameof(StatusPagamentoEnum.FALHA_PAGAMENTO))), Times.Once);
        }

        [Test]
        public async Task Nao_Deve_Realizar_Pagamento_Se_Comando_For_Invalido()
        {
            var command = new RealizarPagamentoMatriculaCommand(
                Guid.Empty,
                Guid.Empty,
                "Cartão teste",
                "1111111111111110",
                DateTime.Now.AddYears(10),
                "123",
                20
            );

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result);
            _financeiroRepositoryMock.Verify(x => x.AdicionarPagamento(It.IsAny<Pagamento>()), Times.Never);
            _mediatorHandlerMock.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }
    }
}
