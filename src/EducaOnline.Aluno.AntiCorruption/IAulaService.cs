using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Aluno.AntiCorruption
{
    public interface IAulaService
    {
        Task<int> ObterTotalAulasPorCurso(Guid cursoId);
        Task<int> ObterHorasAulasPorCurso(Guid cursoId, Guid aulaId);
        Task<string> ObterNomeCurso(Guid cursoId);
    }
}
