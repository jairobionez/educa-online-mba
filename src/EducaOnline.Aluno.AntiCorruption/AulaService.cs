using EducaOnline.Conteudo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.AntiCorruption
{
    public class AulaService : IAulaService
    {
        private readonly IConteudoService _conteudoService;

        public AulaService(IConteudoService conteudoService)
        {
            _conteudoService = conteudoService;
        }

        public async Task<int> ObterTotalAulasPorCurso(Guid cursoId)
        {
            return await _conteudoService.ObterTotalAulasPorCurso(cursoId);
        }

        public async Task<int> ObterHorasAulasPorCurso(Guid cursoId, Guid aulaId)
        {
            return await _conteudoService.ObterHorasAulasPorCurso(cursoId, aulaId);
        }

        public async Task<string> ObterNomeCurso(Guid cursoId)
        {
            var curso = await _conteudoService.BuscarCurso(cursoId);
            return curso?.Nome ?? string.Empty;
        }
    }
}
