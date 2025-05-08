using EducaOnline.Aluno.Domain.ValueObjects;
using EducaOnline.Core.DomainObjects;
using EducaOnline.Core.Enums;

namespace EducaOnline.Aluno.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        protected Aluno()
        {
            DataCadastro = DateTime.Now;
            AulasConcluidas = new HashSet<AulaConcluida>();
        }

        public Aluno(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataCadastro = DateTime.Now;
            AulasConcluidas = new HashSet<AulaConcluida>();
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Ra { get; private set; }
        public string Email { get; private set; }
        public DateTime? DataCadastro { get; private set; }
        public HistoricoAprendizado HistoricoAprendizado { get; private set; }
        public ICollection<AulaConcluida> AulasConcluidas { get; private set; }

        public Matricula Matricula { get; private set; }
        public Certificado Certificado { get; private set; }

        public void RealizarMatricula(Guid cursoId, int totalAulas)
        {
            Matricula = new Matricula(cursoId);
            CriarHistorico(totalAulas);
        }

        public void ConcluirAula(Guid aulaId, int horas)
        {
            if(AulasConcluidas == null)
                AulasConcluidas = new List<AulaConcluida>();


            var aulaConcluida = AulasConcluidas.FirstOrDefault(p => p.AulaId == aulaId);

            if (aulaConcluida != null)
                return;

            AulasConcluidas.Add(new AulaConcluida(aulaId));
            AtualizarHistorico(horas);
        }

        public void AtualizarStatusMatricula(StatusMatriculaEnum status) => Matricula.AtualizarStatus(status);

        public void EmitirCertificado(Certificado certificado)
        {
            if (HistoricoAprendizado.Progresso >= 100 && HistoricoAprendizado.TotalAulas == HistoricoAprendizado.TotalAulasConcluidas)
            {
                AtualizarStatusMatricula(StatusMatriculaEnum.CURSO_CONCLUIDO);
                Certificado = certificado;
            }
            else
                throw new DomainException($"O aluno não finalizou todas as aulas do curso, progresso atual {HistoricoAprendizado.Progresso}");
        }

        public void CriarHistorico(int totalAulas)
        {
            HistoricoAprendizado = new HistoricoAprendizado(0, 0, totalAulas, 0);
        }

        public void AtualizarHistorico(int horas)
        {
            HistoricoAprendizado.Atualizar(horas);
        }

        public void VincularRa(int ra) => Ra = ra;
    }
}
