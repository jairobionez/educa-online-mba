namespace EducaOnline.Conteudo.Application.ViewModels
{
    public class AulaViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid CursoId { get; set; }
    }

    public class AulaResponseViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid CursoId { get; set; }
    }
}
