using EducaOnline.Aluno.AntiCorruption;
using EducaOnline.Aluno.Application.Commands;
using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Communication;
using EducaOnline.Core.DomainObjects;
using EducaOnline.Core.Enums;
using Moq;

namespace EducaOnline.Aluno.Teste
{
    public class AlunoCommandHandlerTests
    {
        private Mock<IAulaService> _aulaServiceMock;
        private Mock<IAlunoRepository> _alunoRepositoryMock;
        private Mock<IMediatorHandler> _mediatorHandler;

        private AlunoCommandHandler _alunoCommandHandler;


        private Guid _cursoId = Guid.NewGuid();
        private Guid _aulaId = Guid.NewGuid();
        private Guid _alunoId = Guid.Parse("8af0a6ba-fd2a-438f-9ebe-d9d3c116bf0f");

        [SetUp]
        public void Setup()
        {
            _aulaServiceMock = new Mock<IAulaService>();
            _aulaServiceMock.Setup(x => x.ObterTotalAulasPorCurso(It.IsAny<Guid>()))
                .ReturnsAsync(20);
            _aulaServiceMock.Setup(x => x.ObterHorasAulasPorCurso(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(1);
            _aulaServiceMock.Setup(x => x.ObterNomeCurso(It.IsAny<Guid>()))
                .ReturnsAsync("Curso de Testes");

            _alunoRepositoryMock = new Mock<IAlunoRepository>();

            _alunoRepositoryMock.Setup(x => x.BuscarAlunoPorId(_alunoId))
                .ReturnsAsync(() =>
                {
                    // Aluno sem matrícula por padrão
                    return new Domain.Aluno(_alunoId, "Jairo Bionez", "jairo@devio.com");
                });

            _alunoRepositoryMock.Setup(x => x.AtualizarAluno(It.IsAny<Domain.Aluno>()));
            _alunoRepositoryMock.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            _mediatorHandler = new Mock<IMediatorHandler>();

            _alunoCommandHandler = new AlunoCommandHandler(
                _mediatorHandler.Object,
                _alunoRepositoryMock.Object,
                _aulaServiceMock.Object
            );
        }

        [Test]
        public async Task RealizarMatricula_DeveRetornarTrue_QuandoComandoValido()
        {
            var command = new RealizarMatriculaCommand(_cursoId, _alunoId);

            var resultado = await _alunoCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(resultado);
            _alunoRepositoryMock.Verify(r => r.AtualizarAluno(It.IsAny<Domain.Aluno>()), Times.Once);
            _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }


        [Test]
        public void RealizarMatricula_DeveLancarExcecao_QuandoAlunoNaoEncontrado()
        {
            var command = new RealizarMatriculaCommand(_cursoId, Guid.NewGuid());
            _alunoRepositoryMock.Setup(r => r.BuscarAlunoPorId(It.IsAny<Guid>()))
                .ReturnsAsync((Domain.Aluno)null);

            var ex = Assert.ThrowsAsync<DomainException>(async () =>
                await _alunoCommandHandler.Handle(command, CancellationToken.None));

            Assert.AreEqual("Aluno não encontrado.", ex.Message);
        }

        [Test]
        public void RealizarMatricula_DeveLancarExcecao_QuandoJaPossuiMatricula()
        {
            var alunoComMatricula = new Domain.Aluno(_alunoId, "Aluno Teste", "email@test.com");
            alunoComMatricula.RealizarMatricula(_cursoId, 10);

            _alunoRepositoryMock.Setup(r => r.BuscarAlunoPorId(_alunoId))
                .ReturnsAsync(alunoComMatricula);

            var command = new RealizarMatriculaCommand(_cursoId, _alunoId);

            var ex = Assert.ThrowsAsync<DomainException>(async () =>
                await _alunoCommandHandler.Handle(command, CancellationToken.None));

            Assert.AreEqual("Aluno já está com uma matricula ativa", ex.Message);
        }

        [Test]
        public async Task ConcluirAula_DeveRetornarTrue_QuandoTudoOk()
        {
            var aluno = new Domain.Aluno(_alunoId, "Aluno Teste", "email@test.com");
            aluno.RealizarMatricula(_cursoId, 10);
            aluno.AtualizarStatusMatricula(StatusMatriculaEnum.EM_ANDAMENTO);

            _alunoRepositoryMock.Setup(r => r.BuscarAlunoPorId(_alunoId))
                .ReturnsAsync(aluno);

            _aulaServiceMock.Setup(a => a.ObterHorasAulasPorCurso(_cursoId, _aulaId))
                .ReturnsAsync(1);

            var command = new ConcluirAulaCommand(_alunoId, _cursoId, _aulaId);

            var result = await _alunoCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
        }

        [Test]
        public void ConcluirAula_DeveLancarExcecao_QuandoAlunoNaoEncontrado()
        {
            var command = new ConcluirAulaCommand(_cursoId, Guid.NewGuid(), _aulaId);
            _alunoRepositoryMock.Setup(r => r.BuscarAlunoPorId(It.IsAny<Guid>()))
                .ReturnsAsync((Domain.Aluno)null);

            var ex = Assert.ThrowsAsync<DomainException>(async () =>
                await _alunoCommandHandler.Handle(command, CancellationToken.None));

            Assert.AreEqual("Aluno não encontrado.", ex.Message);
        }

        [Test]
        public void ConcluirAula_DeveLancarExcecao_QuandoCursoConcluido()
        {
            var aluno = new Domain.Aluno(_alunoId, "Teste", "email@teste.com");
            aluno.RealizarMatricula(_cursoId, 10);
            aluno.AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);

            _alunoRepositoryMock.Setup(r => r.BuscarAlunoPorId(_alunoId))
                .ReturnsAsync(aluno);

            var command = new ConcluirAulaCommand(_alunoId, _cursoId, _aulaId);

            var ex = Assert.ThrowsAsync<DomainException>(async () =>
                await _alunoCommandHandler.Handle(command, CancellationToken.None));

            Assert.AreEqual("Não é possível concluir após a conclusão do curso", ex.Message);
        }


        [Test]
        public async Task EmitirCertificado_DeveEmitir_QuandoProgresso100()
        {
            var aluno = new Domain.Aluno(_alunoId, "Teste", "teste@email.com");
            aluno.RealizarMatricula(_cursoId, 1);
            aluno.AtualizarHistorico(10);

            _alunoRepositoryMock.Setup(r => r.BuscarAlunoPorId(_alunoId))
                .ReturnsAsync(aluno);

            _aulaServiceMock.Setup(a => a.ObterNomeCurso(_cursoId)).ReturnsAsync("Curso Top");

            var command = new EmitirCertificadoCommand(_cursoId, _alunoId);

            var result = await _alunoCommandHandler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result);
            Assert.IsNotNull(aluno.Certificado);
            Assert.AreEqual(StatusMatriculaEnum.CURSO_CONCLUIDO, aluno.Matricula.Status);
        }
    }
}