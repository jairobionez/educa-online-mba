using EducaOnline.Core.Data;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void AdicionarAluno(Domain.Aluno aluno);
        void AtualizarAluno(Domain.Aluno aluno);
        Task<Domain.Aluno?> BuscarAlunoPorRa(string ra);
        Task<Domain.Aluno?> BuscarAlunoPorId(Guid id);
    }
}
