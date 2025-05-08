using EducaOnline.Aluno.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EducaOnline.Aluno.Data.Mappings
{
    public class AlunoMapping : IEntityTypeConfiguration<Domain.Aluno>
    {
        public void Configure(EntityTypeBuilder<Domain.Aluno> builder)
        {
            builder.ToTable("Aluno");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.HistoricoAprendizado)
                .Property(p => p.TotalAulasConcluidas);

            builder.OwnsOne(p => p.HistoricoAprendizado)
               .Property(p => p.TotalHorasEstudadas);

            builder.OwnsOne(p => p.HistoricoAprendizado)
               .Property(p => p.Progresso);

            builder.OwnsOne(p => p.HistoricoAprendizado)
              .Property(p => p.TotalAulas);

            builder.HasOne(p => p.Matricula);

            builder.HasOne(p => p.Certificado);

            builder.HasMany(p => p.AulasConcluidas)
                .WithOne(p => p.Aluno)
                .HasForeignKey(p => p.AlunoId);
        }
    }

    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matricula");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Status);
        }
    }

    public class CertificadoMapping : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificado");

            builder.HasKey(x => x.Id);
        }
    }

    public class AulaConcluidaMapping : IEntityTypeConfiguration<AulaConcluida>
    {
        public void Configure(EntityTypeBuilder<AulaConcluida> builder)
        {
            builder.ToTable("AulaConcluida");

            builder.HasKey(x => x.Id);
        }
    }
}
