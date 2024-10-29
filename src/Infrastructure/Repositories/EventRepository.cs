using EventManagementSystem.Domain.EventAggregate;
using EventManagementSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using EventManagementSystem.Domain.EventAggregate.Exceptions;

namespace EventManagementSystem.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Event> events = new List<Event>();

        public void AddEvent(Event @event) => events.Add(@event);
        public void RemoveEvent(Event @event)
        {
            Event temp = GetEventById(@event.Id) ?? throw new EventDoesNotExistException("An event with given ID does not exist");
            events.Remove(temp);
        }
        public Event GetEventById(Guid id) => events.FirstOrDefault(e => e.Id == id);
        public List<Event> GetEvents() => events.ToList();

        public void UpdateEvent(Event @event)
        {
            if (GetEventById(@event.Id) == null)
            {
                throw new EventDoesNotExistException("An event with given ID does not exist");
            }

            RemoveEvent(@event);
            AddEvent(@event);
        }
    }
}
