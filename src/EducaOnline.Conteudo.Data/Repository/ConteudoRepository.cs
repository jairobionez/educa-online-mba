using EducaOnline.Conteudo.Domain;
using EducaOnline.Core.Data;

namespace EducaOnline.Conteudo.Data.Repository
{
    public class ConteudoRepository : IConteudoRepository
    {
        private readonly ConteudoDbContext _context;

        public ConteudoRepository(ConteudoDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
