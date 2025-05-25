using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Application.Queries
{
    public interface IAlunoQueries
    {
        IQueryable<Domain.Aluno> BuscarAlunos();
        Task<Domain.Aluno> BuscarAluno(Guid alunoId);
    }
}
