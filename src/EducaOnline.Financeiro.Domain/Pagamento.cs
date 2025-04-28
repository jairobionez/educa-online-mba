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

        public Pagamento(Guid cursoId, Guid alunoId, decimal valorPago)
        {
            CursoId = cursoId;
            ValorPago = valorPago;
            AlunoId = alunoId;
        }

        public Guid CursoId { get; private set; }
        public Guid AlunoId { get; private set; }
        public decimal ValorPago { get; private set; }

        public DadosCartao DadosCartao { get; private set; }
        public StatusPagamento StatusPagamento { get; private set; }

        public bool CartaoValido()
        {
            if (DadosCartao.NumeroCartao == "1111111111111111" && DadosCartao.CvvCartao == "123")
                return true;

            return false;
        }

        public void AdicionarDadosCartao(DadosCartao dados) => DadosCartao = dados;
        public void AtualizarStatus(int codigo, string descricao) => StatusPagamento = new StatusPagamento(codigo, descricao);
    }
}
