using EducaOnline.Conteudo.Domain;
using EducaOnline.Core.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Curso> BuscarCurso(Guid id) => await _context.Cursos.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Curso>> BuscarCursos() => await _context.Cursos.ToListAsync();

        public void AdicionarCurso(Curso curso) => _context.Cursos.Add(curso);

        public void AlterarCurso(Curso curso) => _context.Cursos.Update(curso);

        public void RemoverCurso(Curso curso) => _context.Cursos.Remove(curso);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
