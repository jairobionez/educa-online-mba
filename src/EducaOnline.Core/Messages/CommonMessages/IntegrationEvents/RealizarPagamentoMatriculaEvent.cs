using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Core.Messages.CommonMessages.IntegrationEvents
{
    public class RealizarPagamentoMatriculaEvent : IntegrationEvent
    {
        public RealizarPagamentoMatriculaEvent(Guid alunoId, Guid cursoId, string nomeCartao, string numeroCartao, DateTime expiracaoCartao, string cvvCartao, decimal valorCurso)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
            ValorCurso = valorCurso;
        }

        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
        public decimal ValorCurso { get; set; }
    }
}
