using EventManagementSystem.Domain.ClientEntity;
using EventManagementSystem.Domain.EventAggregate.ValueObjects;
using System;
using System.Collections.Generic;

namespace EventManagementSystem.Contracts
{
    public class EventRequest
    {
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public EventType EventType { get; set; }
        public Guid AssignedManagerId { get; set; }
        public List<ServiceRequest> Services { get; set; }
    }
}
