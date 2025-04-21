using EducaOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Domain
{
    public class Aula : Entity
    {
        protected Aula()
        {
            
        }

        public Aula(string titulo, string descricao)
        {
            Titulo = titulo;
            Descricao = descricao;
        }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }

        public Guid CursoId { get; private set; }
        public Curso Curso { get; private set; }
    }
}
