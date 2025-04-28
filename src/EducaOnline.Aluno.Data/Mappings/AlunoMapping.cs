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
               .Property(p => p.MediaAproveitamento);

            builder.HasMany(p => p.Matriculas)
                .WithOne(p => p.Aluno)
                .HasForeignKey(p => p.AlunoId);

            builder.HasMany(p => p.Certificados)
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

            builder.Property(p => p.Vigente)
                .HasDefaultValue(false);
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
}
