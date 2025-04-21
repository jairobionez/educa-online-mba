using EducaOnline.Core.DomainObjects;
using EducaOnline.Financeiro.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Financeiro.Domain
{
    public class Pagamento : Entity, IAggregateRoot
    {
        protected Pagamento()
        {
            
        }

        public Pagamento(Guid cursoId, decimal valorPago)
        {
            CursoId = cursoId;
            ValorPago = valorPago;
        }

        public Guid CursoId { get; private set; }
        public Guid AlunoId { get; private set; }
        public decimal ValorPago { get; private set; }

        public DadosCartao DadosCartao { get; private set; }
        public StatusPagamento StatusPagamento { get; private set; }
    }
}
