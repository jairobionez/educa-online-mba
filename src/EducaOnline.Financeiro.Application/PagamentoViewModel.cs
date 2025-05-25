namespace EducaOnline.Financeiro.Application
{
    public class PagamentoViewModel
    {
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
        public decimal ValorCurso { get; set; }
    }
}
