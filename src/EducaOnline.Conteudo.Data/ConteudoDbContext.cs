using EducaOnline.Conteudo.Domain;
using EducaOnline.Core.Data;
using EducaOnline.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Conteudo.Data
{
    public class ConteudoDbContext : DbContext, IUnitOfWork
    {
        public ConteudoDbContext(DbContextOptions<ConteudoDbContext> options)
            : base(options) { }


        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aula { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConteudoDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
