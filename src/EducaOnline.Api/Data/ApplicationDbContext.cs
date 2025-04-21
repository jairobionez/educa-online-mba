using EducaOnline.Aluno.Data;
using EducaOnline.Api.Models;
using EducaOnline.Core.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.Entity<Administrador>()
                        .ToTable(nameof(Administrador));

             modelBuilder.Entity<Administrador>()
                        .HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
