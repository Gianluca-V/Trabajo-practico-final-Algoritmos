using EventManagementSystem.Domain.EventAggregate;
using System;
using System.Collections.Generic;

namespace EventManagementSystem.Domain.Repositories
{
    public interface IEventRepository
    {
        void AddEvent(Event @event);
        void RemoveEvent(Event @event);
        Event GetEventById(Guid id);
        List<Event> GetEvents();
        void UpdateEvent(Event @event);
    }
}
