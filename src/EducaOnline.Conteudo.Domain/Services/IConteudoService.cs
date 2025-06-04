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
        Task<Curso> AlterarNomeCurso(Guid id, string nome);
        Task<Curso> AlterarConteudoProgramaticoCurso(Guid id, ConteudoProgramatico conteudoProgramatico);
        Task DesativarCurso(Guid id);
        Task<Curso> AdicionarAula(Guid cursoId, Aula aula);
        Task<Curso> AlterarAula(Guid cursoId, Guid aulaId, Aula aula);
        Task<Curso> RemoverAula(Guid cursoId, Guid aulaId);
        Task<int> ObterTotalAulasPorCurso(Guid cursoId);
        Task<int> ObterHorasAulasPorCurso(Guid cursoId, Guid aulaId);
    }
}
