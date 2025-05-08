using EducaOnline.Conteudo.Domain.ValueObjects;
using EducaOnline.Core.DomainObjects;

namespace EducaOnline.Conteudo.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        protected Curso()
        {
            Aulas = new HashSet<Aula>();
        }

        public Curso(Guid? id, string? nome, ConteudoProgramatico? conteudoProgramatico, bool ativo)
        {
            Id = id;
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            Aulas = new HashSet<Aula>();
            Ativo = ativo;
        }

        public Guid? Id { get; private set; }
        public string? Nome { get; private set; }
        public bool Ativo { get; private set; }

        public ConteudoProgramatico? ConteudoProgramatico { get; private set; }
        public ICollection<Aula>? Aulas { get; private set; }


        public void Atualizar(string nome)
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do curso não pode estar vazio");

            Nome = nome;
        }

        public void AdicionarAula(Aula aula)
        {
            Aulas.Add(aula);
        }

        public void AlterarAula(Guid aulaId, Aula aula)
        {
            foreach (var aulaDomain in Aulas.Where(p => p.Id == aulaId))
                aulaDomain.Atualizar(aula);
        }

        public void RemoverAula(Guid aulaId)
        {
            var aula = Aulas.FirstOrDefault(p => p.Id == aulaId);

            if(aula == null)
                throw new DomainException("Aula não encontrada");

            Aulas.Remove(aula);
        }

        public void AtualizarConteudoProgramatico(ConteudoProgramatico conteudoProgramatico)
        {
            ValidarConteudoProgramtico();

            ConteudoProgramatico = conteudoProgramatico;
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do curso não pode estar vazio");
            ValidarConteudoProgramtico();

            if (!Aulas.Any())
                throw new DomainException("É necessário adicionar pelo menos uma aula ao curso");
        }

        public void ValidarConteudoProgramtico()
        {
            Validacoes.ValidarSeVazio(ConteudoProgramatico.Titulo, "O campo Titulo do conteudo programtico não pode estar vazio");
            Validacoes.ValidarSeVazio(ConteudoProgramatico.Descricao, "O campo Descricao do conteudo programtico não pode estar vazio");
            Validacoes.ValidarMinimoMaximo(ConteudoProgramatico.CargaHoraria, 1, 2000, "O campo CargaHorario do conteudo programtico deve estar dentro de 1 hora até 2000 horas");
            Validacoes.ValidarSeVazio(ConteudoProgramatico.Objetivos, "O campo Objetivos do conteudo programtico não pode estar vazio");
        }
    }
}
