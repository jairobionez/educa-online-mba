using EducaOnline.Conteudo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Domain.Services
{
    public interface IConteudoService
    {
        Task<List<Curso>> BuscarCursos();
        Task<Curso> BuscarCurso(Guid id);
        Task AdicionarCurso(Curso curso);
        Task AlterarNomeCurso(Guid id, string nome);
        Task AlterarConteudoProgramaticoCurso(Guid id, ConteudoProgramatico conteudoProgramatico);
        Task DesativarCurso(Guid id);
        Task<bool> AdicionarAula(Guid cursoId, Aula aula);
        Task<bool> AlterarAula(Guid cursoId, Guid aulaId, Aula aula);
        Task<bool> RemoverAula(Guid cursoId, Guid aulaId);
    }
}
