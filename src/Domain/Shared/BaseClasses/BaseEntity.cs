using System;

namespace EventManagementSystem.Domain.Shared.BaseClasses
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
