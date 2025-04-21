using EducaOnline.Core.Messages;
using EducaOnline.Core.Messages.CommonMessages.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoMatriculaEvent : DomainEvent
    {
        public string Status { get; set; }

        public PagamentoMatriculaEvent(Guid aggregateId, string status) : base(aggregateId)
        {
            Status = status;
        }
    }
}
