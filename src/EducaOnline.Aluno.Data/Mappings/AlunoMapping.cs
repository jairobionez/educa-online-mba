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


            builder
                .OwnsMany(a => a.Matriculas, b =>
                {
                    b.WithOwner().HasForeignKey("AlunoId");
                    b.HasKey("Id");
                });

            builder
                .OwnsMany(a => a.Certificados, b =>
                {
                    b.WithOwner().HasForeignKey("AlunoId");
                    b.HasKey("Id");
                });

            builder.OwnsOne(p => p.HistoricoAprendizado)
                .Property(p => p.TotalAulasConcluidas);

            builder.OwnsOne(p => p.HistoricoAprendizado)
               .Property(p => p.TotalHorasEstudadas);

            builder.OwnsOne(p => p.HistoricoAprendizado)
               .Property(p => p.MediaAproveitamento);
        }
    }
}
