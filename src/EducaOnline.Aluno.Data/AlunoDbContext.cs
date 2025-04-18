using EducaOnline.Core.Data;
using EducaOnline.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Aluno.Data
{
    public class AlunoDbContext : DbContext, IUnitOfWork
    {
        public AlunoDbContext(DbContextOptions<AlunoDbContext> options)
            : base(options) { }

        public DbSet<Domain.Aluno> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlunoDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
