﻿using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Data;
using EducaOnline.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Aluno.Data
{
    public class AlunoDbContext : DbContext, IUnitOfWork
    {
        public AlunoDbContext(DbContextOptions<AlunoDbContext> options)
            : base(options) { }

        public DbSet<Domain.Aluno> Alunos { get; set; }
        public DbSet<Certificado> Certificados { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<AulaConcluida> AulasConcluidas { get; set; }

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
