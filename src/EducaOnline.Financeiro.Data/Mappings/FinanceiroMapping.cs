using EducaOnline.Financeiro.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Financeiro.Data.Mappings
{
    public class FinanceiroMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamento");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.DadosCartao)
                .Property(p => p.NumeroCartao);

            builder.OwnsOne(p => p.DadosCartao)
               .Property(p => p.CvvCartao);

            builder.OwnsOne(p => p.DadosCartao)
               .Property(p => p.ExpiracaoCartao);

            builder.OwnsOne(p => p.DadosCartao)
               .Property(p => p.NomeCartao);

            builder.OwnsOne(p => p.StatusPagamento)
                .Property(p => p.Descricao);

            builder.OwnsOne(p => p.StatusPagamento)
             .Property(p => p.Codigo);
        }
    }
}
