namespace EducaOnline.Api.Models
{
    public class Administrador
    {
        protected Administrador()
        {
            Ativo = true;
        }

        public Administrador(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Ativo = true;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
