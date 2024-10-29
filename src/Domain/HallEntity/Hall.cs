using EventManagementSystem.Domain.EventAggregate;
using EventManagementSystem.Domain.Shared.BaseClasses;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace EventManagementSystem.Domain.HallEntity
{
    public class Hall : BaseEntity
    {
        public Name name { get; }
        public string address { get; }
        private List<Event> events { get; }
        public HashSet<DateTime> notAvailableDates { get; }

        private Hall(Name name, string address)
        {
            this.name = name;
            this.address = address;
            events = new List<Event>();
            notAvailableDates = new HashSet<DateTime>();
        }

        public static Hall Make(string name, string address)
        {
            return new Hall(Name.Make(name), address);
        }

        public void AddEvent(Event e)
        {
            this.events.Add(e);
            this.notAvailableDates.Add(e.Date.Value.Date);
        }

        public void RemoveEvent(Event e)
        {
            this.events.Remove(e);
            this.notAvailableDates.Remove(e.Date.Value.Date);
        }
    }
}
