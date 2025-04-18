using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Domain.ValueObjects
{
    public class HistoricoAprendizado
    {
        public int TotalHorasEstudadas { get; private set; }
        public int TotalAulasConcluidas { get; private set; }
        public double MediaAproveitamento { get; private set; }

        private HistoricoAprendizado() { }

        public HistoricoAprendizado(int totalHoras, int totalAulas, double mediaAproveitamento)
        {
            TotalHorasEstudadas = totalHoras;
            TotalAulasConcluidas = totalAulas;
            MediaAproveitamento = mediaAproveitamento;
        }

        public HistoricoAprendizado Atualizar(int novasHoras, int novasAulas, double novaMedia)
        {
            return new HistoricoAprendizado(
                TotalHorasEstudadas + novasHoras,
                TotalAulasConcluidas + novasAulas,
                novaMedia
            );
        }
    }
}
