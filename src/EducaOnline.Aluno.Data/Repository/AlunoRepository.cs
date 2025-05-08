using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Aluno.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AlunoDbContext _context;

        public AlunoRepository(AlunoDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void AdicionarAluno(Domain.Aluno aluno)
        {
            _context.Alunos.Add(aluno);
        }

        public void AtualizarAluno(Domain.Aluno aluno)
        {
            _context.Alunos.Update(aluno);
        }

        public async Task<int> BuscarUltimoRa()
        {
            return await _context.Alunos.MaxAsync(p => p.Ra);
        }

        public IQueryable<Domain.Aluno?> BuscarAlunos()
        {
            return _context.Alunos;
        }

        public async Task<Domain.Aluno?> BuscarAlunoPorId(Guid id)
        {
            return await _context.Alunos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Domain.Aluno?> BuscarAlunoPorRa(int ra)
        {
            return await _context.Alunos.FirstOrDefaultAsync(p => p.Ra == ra);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
