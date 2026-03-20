using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity
{
    public abstract class Entity 
    {
        private readonly List<IDomainEvent> _domainEvent = new();
        public IReadOnlyCollection<IDomainEvent> GetDomainEvent => _domainEvent.ToList();
        public List <AuditLogEntity> _auditLog  => _auditLog.ToList();

         

        public void ClearDomainEvent()
        {
            _domainEvent.Clear();
        }

        protected void RaiseDoaminEvent(IDomainEvent domainEvent)
        {
            _domainEvent.Add(domainEvent);
        }


    }
}
