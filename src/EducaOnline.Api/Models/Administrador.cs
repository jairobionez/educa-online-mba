namespace EducaOnline.Api.Models
{
    public class Administrador
    {
        protected Administrador()
        {
            
        }

        public Administrador(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
