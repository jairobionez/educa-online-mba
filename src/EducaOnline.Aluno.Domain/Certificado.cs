using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.Domain
{
    public class Certificado
    {
        protected Certificado()
        {
            
        }

        public Certificado(Guid cursoId)
        {
            Id = Guid.NewGuid();
            CursoId = cursoId;
            DataEmissao = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid AlunoId { get; private set; }
        public DateTime DataEmissao { get; private set; }

        public Aluno Aluno { get; private set; }
    }
}
