using EducaOnline.Aluno.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Application.Queries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoQueries(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public IQueryable<Domain.Aluno> BuscarAlunos()
        {
            return _alunoRepository.BuscarAlunos();
        }

        public async Task<Domain.Aluno> BuscarAluno(Guid alunoId)
        {
            return await _alunoRepository.BuscarAlunoPorId(alunoId);
        }
    }
}
