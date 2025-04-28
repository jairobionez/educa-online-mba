using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoMatriculaEvent : IntegrationEvent
    {
        public PagamentoMatriculaEvent(Guid cursoId, Guid alunoId, string status)
        {
            CursoId = cursoId;
            Status = status;
            AlunoId = alunoId;
        }

        public string Status { get; set; }
        public Guid CursoId { get; set; }
        public Guid AlunoId { get; set; }
    }
}
