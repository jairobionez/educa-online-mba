using EducaOnline.Core.Data;

namespace EducaOnline.Aluno.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void Adicionar(Domain.Aluno aluno);
    }
}
