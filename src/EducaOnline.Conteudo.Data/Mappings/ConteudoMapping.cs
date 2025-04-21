using EducaOnline.Conteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.ConteudoProgramatico)
                .Property(p => p.Titulo);

            builder.OwnsOne(p => p.ConteudoProgramatico)
                .Property(p => p.Descricao);

            builder.OwnsOne(p => p.ConteudoProgramatico)
                .Property(p => p.Objetivos);

            builder.OwnsOne(p => p.ConteudoProgramatico)
                .Property(p => p.CargaHoraria);

            builder.HasMany(p => p.Aulas)
                .WithOne(p => p.Curso)
                .HasForeignKey(p => p.CursoId);
        }
    }

    public class AulaMapping : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.ToTable("Aula");

            builder.HasKey(x => x.Id);
        }
    }
}
