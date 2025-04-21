using EducaOnline.Conteudo.Domain.ValueObjects;
using EducaOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        protected Curso()
        {
            Aulas = new HashSet<Aula>();
        }

        public Curso(Guid? id, string? nome, ConteudoProgramatico? conteudoProgramatico)
        {
            Id = id;
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            Aulas = new HashSet<Aula>();
        }

        public Guid? Id { get; private set; }
        public string? Nome { get; private set; }

        public ConteudoProgramatico? ConteudoProgramatico { get; private set; }

        public ICollection<Aula>? Aulas { get; set; }

        public void AdicionarAula(Aula aula)
        {
            Aulas.Add(aula);
        }

        public void AtualizarConteudoProgramatico(ConteudoProgramatico conteudoProgramatico)
        {
            ConteudoProgramatico = conteudoProgramatico;
        }
    }
}
