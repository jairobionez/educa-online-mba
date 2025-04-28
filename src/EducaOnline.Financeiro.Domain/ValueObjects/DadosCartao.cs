using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Financeiro.Domain.ValueObjects
{
    public class DadosCartao
    {
        public DadosCartao(string nomeCartao, string numeroCartao, DateTime expiracaoCartao, string cvvCartao)
        {
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
    }
}
