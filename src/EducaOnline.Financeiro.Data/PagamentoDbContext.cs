using EducaOnline.Core.Data;
using EducaOnline.Core.Messages;
using EducaOnline.Financeiro.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Financeiro.Data
{
    public class PagamentoDbContext : DbContext, IUnitOfWork
    {
        public PagamentoDbContext(DbContextOptions<PagamentoDbContext> options)
            : base(options) { }

        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
