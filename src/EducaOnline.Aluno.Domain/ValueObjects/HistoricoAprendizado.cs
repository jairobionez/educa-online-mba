namespace EducaOnline.Aluno.Domain.ValueObjects
{
    public class HistoricoAprendizado
    {
        public int TotalHorasEstudadas { get; private set; }
        public int TotalAulasConcluidas { get; private set; }
        public double Progresso { get; private set; }
        public int TotalAulas { get; private set; }

        private HistoricoAprendizado() { }

        public HistoricoAprendizado(int totalHoras, int totalAulasConcluidas, int totalAulas, double progresso = 0)
        {
            TotalHorasEstudadas = totalHoras;
            TotalAulasConcluidas = totalAulasConcluidas;
            TotalAulasConcluidas = totalAulas;
            Progresso = progresso;
        }

        public void Atualizar(int novasHoras)
        {
            TotalHorasEstudadas += 1;
            TotalAulasConcluidas += 1;
            Progresso = (TotalAulasConcluidas * 100) / TotalAulas;
        }
    }
}
