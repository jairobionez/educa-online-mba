using AutoMapper;
using EducaOnline.Conteudo.Application.ViewModels;
using EducaOnline.Conteudo.Domain;
using EducaOnline.Conteudo.Domain.Services;
using EducaOnline.Conteudo.Domain.ValueObjects;
using EducaOnline.Core.DomainObjects;
using System.Threading.Tasks;

namespace EducaOnline.Conteudo.Application.Services
{
    public class ConteudoAppService : IConteudoAppService
    {
        private readonly IConteudoService _conteudoService;
        private readonly IMapper _mapper;

        public ConteudoAppService(IConteudoService conteudoService, IMapper mapper)
        {
            _conteudoService = conteudoService;
            _mapper = mapper;
        }

        public async Task<CursoResponseViewModel> BuscarCursos()
        {
            return _mapper.Map<CursoResponseViewModel>(await _conteudoService.BuscarCursos());
        }

        public async Task<CursoResponseViewModel> BuscarCurso(Guid id)
        {
            return _mapper.Map<CursoResponseViewModel>(await _conteudoService.BuscarCurso(id));
        }

        public async Task AdicionarCurso(CursoViewModel model)
        {
            await _conteudoService.AdicionarCurso(_mapper.Map<Curso>(model));
        }

        public async Task AlterarNomeCurso(Guid id, CursoNomeViewModel model)
        {
            await _conteudoService.AlterarNomeCurso(id, model.Nome);
        }

        public async Task AlterarConteudoProgramticoCurso(Guid id, ConteudoProgramaticoViewModel conteudoProgramatico)
        {
            await _conteudoService.AlterarConteudoProgramaticoCurso(id, _mapper.Map<ConteudoProgramatico>(conteudoProgramatico));
        }

        public async Task DesativarCurso(Guid id)
        {
            // TODO: verificar se possui algum aluno cursando
            await _conteudoService.DesativarCurso(id);
        }

        public async Task<CursoResponseViewModel> AdicionarAula(Guid cursoId, AulaViewModel model)
        {
            if (!(await _conteudoService.AdicionarAula(cursoId, _mapper.Map<Aula>(model))))
                throw new DomainException("Falha ao adicionar aula");

            return _mapper.Map<CursoResponseViewModel>(await _conteudoService.BuscarCurso(cursoId));
        }

        public async Task<CursoResponseViewModel> AlterarAula(Guid cursoId, Guid aulaId, AulaViewModel model)
        {
            if (!(await _conteudoService.AlterarAula(cursoId, aulaId, _mapper.Map<Aula>(model))))
                throw new DomainException("Falha ao alterar aula");

            return _mapper.Map<CursoResponseViewModel>(await _conteudoService.BuscarCurso(cursoId));
        }

        public async Task<CursoResponseViewModel> RemoverAula(Guid cursoId, Guid aulaId)
        {
            if (!(await _conteudoService.RemoverAula(cursoId, aulaId)))
                throw new DomainException("Falha ao remover aula");

            return _mapper.Map<CursoResponseViewModel>(await _conteudoService.BuscarCurso(cursoId));
        }
    }
}
