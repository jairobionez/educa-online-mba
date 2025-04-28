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

        public async Task<Domain.Aluno?> BuscarAlunoPorId(Guid id)
        {
            return await _context.Alunos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Domain.Aluno?> BuscarAlunoPorRa(string ra)
        {
            return await _context.Alunos.FirstOrDefaultAsync(p => p.Ra == ra);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
