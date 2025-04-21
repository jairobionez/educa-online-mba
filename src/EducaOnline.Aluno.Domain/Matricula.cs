using EducaOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Domain
{
    public class Matricula : Entity
    {
        protected Matricula()
        {
            DataMatricula = DateTime.Now;
        }

        public Matricula(Guid cursoId)
        {
            Id = Guid.NewGuid();
            CursoId = cursoId;
            DataMatricula = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid CursoId { get; private set; }
        public DateTime DataMatricula { get; private set; }
        public bool Ativa { get; private set; }

        public Guid AlunoId { get; private set; }
        public Aluno Aluno { get; private set; }
    }
}
