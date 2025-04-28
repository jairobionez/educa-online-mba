using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Financeiro.Domain.ValueObjects
{
    public class StatusPagamento
    {
        public StatusPagamento(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
