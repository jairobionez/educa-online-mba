using EducaOnline.Aluno.Domain.ValueObjects;
using EducaOnline.Core.DomainObjects;

namespace EducaOnline.Aluno.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        protected Aluno()
        {
            Matriculas = new HashSet<Matricula>();
            Certificados = new HashSet<Certificado>();
            DataCadastro = DateTime.Now;
        }

        public Aluno(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Matriculas = new HashSet<Matricula>();
            Certificados = new HashSet<Certificado>();
            DataCadastro = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Ra { get; private set; }
        public DateTime? DataCadastro { get; private set; }
        public HistoricoAprendizado HistoricoAprendizado { get; private set; }

        public ICollection<Matricula> Matriculas { get; private set; }
        public ICollection<Certificado> Certificados { get; private set; }

        public void Matricular(Matricula matricula)
        {
            Matriculas.Add(matricula);
        }

        public void EmitirCertificado(Certificado certificado)
        {
            Certificados.Add(certificado);
        }

        public void AtualizarHistorico(int horas, int aulas, double media)
        {
            HistoricoAprendizado = HistoricoAprendizado.Atualizar(horas, aulas, media);
        }
    }
}
