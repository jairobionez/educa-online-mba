using EducaOnline.Core.Data;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void AdicionarAluno(Domain.Aluno aluno);
        void AtualizarAluno(Domain.Aluno aluno);
        IQueryable<Domain.Aluno?> BuscarAlunos();
        Task<Domain.Aluno?> BuscarAlunoPorRa(int ra);
        Task<Domain.Aluno?> BuscarAlunoPorId(Guid id);
        Task<int> BuscarUltimoRa();
    }
}
