﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducaOnline.Core.Messages.CommonMessages.DomainEvents
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
