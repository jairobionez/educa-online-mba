using EducaOnline.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Domain
{
    public interface IConteudoRepository : IRepository<Curso>
    {
        void AdicionarCurso(Curso curso);
        void AlterarCurso(Curso curso);
        Task<List<Curso>> BuscarCursos();
        Task<Curso> BuscarCurso(Guid id);
        void RemoverCurso(Curso curso);
    }
}
