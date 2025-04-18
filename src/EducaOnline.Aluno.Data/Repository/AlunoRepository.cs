using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Data;

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

        public void Adicionar(Domain.Aluno aluno)
        {
            _context.Set<Domain.Aluno>().Add(aluno);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
