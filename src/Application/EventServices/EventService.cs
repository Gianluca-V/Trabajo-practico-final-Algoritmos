using EventManagementSystem.Contracts;
using EventManagementSystem.Domain.HallEntity;
using EventManagementSystem.Domain.ManagerEntity;
using EventManagementSystem.Domain.Repositories;
using EventManagementSystem.Domain.ServiceEntity;
using EventManagementSystem.Domain.EventAggregate;
using System;
using System.Collections.Generic;
using EventManagementSystem.Domain.EventAggregate.ValueObjects;
using EventManagementSystem.Domain.EventAggregate.Exceptions;
using EventManagementSystem.Domain.EmployeeEntity.Exceptions;
using EventManagementSystem.Domain.Shared.ValueObjects;

namespace EventManagementSystem.Application.EventServices
{
    public class EventService
    {
        private readonly IEventRepository eventRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IClientRepository clientRepository;
        private readonly Hall hall;

        public EventService(
            IEventRepository eventRepository,
            IEmployeeRepository employeeRepository,
            IClientRepository clientRepository,
            Hall hall)
        {
            this.eventRepository = eventRepository;
            this.employeeRepository = employeeRepository;
            this.clientRepository = clientRepository;
            this.hall = hall;
        }

        public Event CreateEvent(EventRequest request)
        {
            var client = request.Client;

            Manager manager = (Manager) employeeRepository.GetEmployeeById(request.AssignedManagerId) ??
                         throw new ArgumentException("Invalid manager ID");

            var @event = Event.Make(
                client,
                request.Date,
                request.StartTime,
                request.EndTime,
                request.EventType,
                manager);

            foreach (var serviceRequest in request.Services)
            {
                var service = Service.Make(
                    serviceRequest.ServiceType,
                    serviceRequest.Description,
                    serviceRequest.Quantity,
                    serviceRequest.UnitCost);

                @event.AddService(service);
            }

            eventRepository.AddEvent(@event);
            hall.AddEvent(@event);
            return @event;
        }

        public bool IsDateAvailable(EventDateTime date)
        {
            return !hall.notAvailableDates.Contains(date.Value.Date);
        }

        public Event GetEventById(Guid id) => eventRepository.GetEventById(id);
        public List<Event> GetEvents()
        {
            var events = eventRepository.GetEvents();

            var n = events.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (events[j].Date.Value > events[j+1].Date.Value)
                    {
                        var temp = events[i];
                        events[i] = events[j];
                        events[j] = temp;
                    }
                }
            }

            return events;
        }

        public void UpdateEvent(Guid id, EventRequest request)
        {
            var @event = eventRepository.GetEventById(id) ?? throw new EventDoesNotExistException("Event not found");

            // Update event properties
            @event.Date = EventDateTime.Make(request.Date);
            @event.Time = EventTimeSpan.Make(request.StartTime, request.EndTime);

            // Update manager
            var newManager = (Manager)employeeRepository.GetEmployeeById(request.AssignedManagerId) ??
                             throw new EmployeeDoesNotExistException("Invalid manager ID");

            @event.AssignedManager = newManager;

            @event.ClearServices();
            foreach (var serviceRequest in request.Services)
            {
                var service = Service.Make(
                    serviceRequest.ServiceType,
                    serviceRequest.Description,
                    serviceRequest.Quantity,
                    serviceRequest.UnitCost);

                @event.AddService(service);
            }

            eventRepository.UpdateEvent(@event);
        }

        public void CancelEvent(Guid id)
        {
            var @event = eventRepository.GetEventById(id) ?? throw new EventDoesNotExistException("Event not found");

            var daysBeforeEvent = (@event.Date.Value.Date - DateTime.Now.Date).TotalDays;

            Money clientPayment = daysBeforeEvent > 30
                ? @event.DownPayment
                : @event.TotalCost;

            Console.WriteLine($"The client is required to pay: {clientPayment.ToString()}");

            hall.RemoveEvent(@event);
            eventRepository.RemoveEvent(@event);
        }
    }
}
