using EducaOnline.Conteudo.Application.ViewModels;
using EducaOnline.Conteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Application.Services
{
    public interface IConteudoAppService
    {
        Task<List<CursoResponseViewModel>> BuscarCursos();
        Task<CursoResponseViewModel> BuscarCurso(Guid id);
        Task AdicionarCurso(CursoViewModel model);
        Task<Curso> AlterarNomeCurso(Guid id, CursoNomeViewModel model);
        Task<Curso> AlterarConteudoProgramticoCurso(Guid id, ConteudoProgramaticoViewModel conteudoProgramatico);
        Task DesativarCurso(Guid id);
        Task<CursoResponseViewModel> AdicionarAula(Guid cursoId, AulaViewModel model);
        Task<CursoResponseViewModel> AlterarAula(Guid cursoId, Guid aulaId, AulaViewModel model);
        Task<CursoResponseViewModel> RemoverAula(Guid cursoId, Guid aulaId);
    }
}
