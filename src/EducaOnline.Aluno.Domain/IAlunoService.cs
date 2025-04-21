using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Domain
{
    public interface IAlunoService
    {
        Task AdicionarAluno(Aluno aluno);
        Task<Domain.Aluno> BuscarAlunoPorRa(string ra);
        Task<Domain.Aluno> BuscarAlunoPorId(Guid id);
    }
}
