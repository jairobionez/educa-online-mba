using EducaOnline.Conteudo.Application.Services;
using EducaOnline.Conteudo.Domain;
using EducaOnline.Conteudo.Domain.Services;
using EducaOnline.Core.DomainObjects;
using Moq;

namespace EducaOnline.Conteudo.Teste
{
    public class ConteudoServiceTests
    {
        private IConteudoService _conteudoService;
        private Mock<IConteudoRepository> _conteudoRepositoryMock;

        private readonly Guid _cursoId = Guid.Parse("8af0a6ba-fd2a-438f-9ebe-d9d3c116bf0f");

        [SetUp]
        public void Setup()
        {
            _conteudoRepositoryMock = new Mock<IConteudoRepository>();

            var cursos = new List<Curso>
            {
                CriarCurso(_cursoId, "Teste 01", 20),
                CriarCurso(Guid.NewGuid(), "Teste 02", 30),
                CriarCurso(Guid.NewGuid(), "Teste 03", 40),
                CriarCurso(Guid.NewGuid(), "Teste 04", 25),
            };

            _conteudoRepositoryMock.Setup(x => x.BuscarCursos()).ReturnsAsync(cursos);

            _conteudoRepositoryMock.Setup(x => x.BuscarCurso(_cursoId))
                .ReturnsAsync(CriarCurso(_cursoId, "Teste 01", 20));

            _conteudoRepositoryMock.Setup(x => x.UnitOfWork.Commit()).ReturnsAsync(true);

            _conteudoService = new ConteudoService(_conteudoRepositoryMock.Object);
        }

        private Curso CriarCurso(Guid id, string nome, int cargaHoraria)
        {
            return new Curso(id, nome,
                new Domain.ValueObjects.ConteudoProgramatico($"Conteúdo {nome}", "Descrição teste", cargaHoraria, "Objetivos teste"), true);
        }

        [Test]
        public async Task Deve_Buscar_Todos_Os_Cursos()
        {
            var cursos = await _conteudoService.BuscarCursos();

            Assert.AreEqual(4, cursos.Count());
        }

        [Test]
        public async Task Deve_Buscar_Curso_Por_Id()
        {
            var curso = await _conteudoService.BuscarCurso(_cursoId);

            Assert.AreEqual(_cursoId, curso.Id);
        }

        [Test]
        public void Nao_Deve_Adicionar_Curso_Sem_Aula()
        {
            var curso = CriarCurso(Guid.NewGuid(), "Teste Sem Aula", 50);

            var ex = Assert.ThrowsAsync<DomainException>(() =>
                _conteudoService.AdicionarCurso(curso));

            Assert.That(ex.Message, Is.Not.Empty);
        }

        [Test]
        public void Deve_Adicionar_Curso_Com_Aula()
        {
            var curso = CriarCurso(Guid.NewGuid(), "Teste 05", 50);
            curso.AdicionarAula(new Aula("Aula teste", "Descrição", 50));

            Assert.DoesNotThrowAsync(() => _conteudoService.AdicionarCurso(curso));
        }

        [Test]
        public async Task Deve_Alterar_Nome_Do_Curso()
        {
            var novoNome = "Curso Editado";

            var cursoEditado = await _conteudoService.AlterarNomeCurso(_cursoId, novoNome);

            Assert.AreEqual(novoNome, cursoEditado.Nome);
        }

        [Test]
        public void Nao_Deve_Alterar_ConteudoProgramatico_Com_Valores_Invalidos()
        {
            var conteudoInvalido = new Domain.ValueObjects.ConteudoProgramatico("", "", 20, "");

            var ex = Assert.ThrowsAsync<DomainException>(() =>
                _conteudoService.AlterarConteudoProgramaticoCurso(_cursoId, conteudoInvalido));

            Assert.That(ex.Message, Is.Not.Empty);
        }

        [Test]
        public async Task Deve_Alterar_ConteudoProgramatico()
        {
            var novoConteudo = new Domain.ValueObjects.ConteudoProgramatico("Editado", "Editado", 20, "Editado");

            var curso = await _conteudoService.AlterarConteudoProgramaticoCurso(_cursoId, novoConteudo);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Editado", curso.ConteudoProgramatico.Titulo);
                Assert.AreEqual("Editado", curso.ConteudoProgramatico.Descricao);
                Assert.AreEqual(20, curso.ConteudoProgramatico.CargaHoraria);
                Assert.AreEqual("Editado", curso.ConteudoProgramatico.Objetivos);
            });
        }

        [Test]
        public async Task Deve_Adicionar_Aula_Ao_Curso()
        {
            var aula = new Aula("Aula 01", "Descrição", 20);

            var curso = await _conteudoService.AdicionarAula(_cursoId, aula);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, curso.Aulas.Count());
                Assert.AreEqual(20, curso.ConteudoProgramatico.CargaHoraria);
            });
        }

        [Test]
        public async Task Deve_Alterar_Aula_Do_Curso()
        {
            var aula = new Aula("Aula 01", "Descrição", 20);
            var curso = await _conteudoService.AdicionarAula(_cursoId, aula);

            var novaAula = new Aula("Aula Editada", "Descrição Editada", 15);
            curso = await _conteudoService.AlterarAula(curso.Id.Value, aula.Id, novaAula);

            var aulaEditada = curso.Aulas.FirstOrDefault(a => a.Id == aula.Id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, curso.Aulas.Count());
                Assert.AreEqual(15, curso.ConteudoProgramatico.CargaHoraria);
                Assert.AreEqual("Aula Editada", aulaEditada.Titulo);
                Assert.AreEqual("Descrição Editada", aulaEditada.Descricao);
            });
        }

        [Test]
        public async Task Deve_Remover_Aula_Do_Curso()
        {
            var aula = new Aula("Aula 01", "Descrição", 20);
            var curso = await _conteudoService.AdicionarAula(_cursoId, aula);

            curso = await _conteudoService.RemoverAula(curso.Id.Value, aula.Id);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, curso.Aulas.Count());
                Assert.AreEqual(0, curso.ConteudoProgramatico.CargaHoraria);
            });
        }
    }
}
