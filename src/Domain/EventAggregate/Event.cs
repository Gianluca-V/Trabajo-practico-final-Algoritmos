using EventManagementSystem.Domain.ClientEntity;
using EventManagementSystem.Domain.EventAggregate.ValueObjects;
using EventManagementSystem.Domain.ManagerEntity;
using EventManagementSystem.Domain.ServiceEntity;
using EventManagementSystem.Domain.Shared.BaseClasses;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementSystem.Domain.EventAggregate

{
    public class Event : BaseEntity
    {
        private Client client;
        private EventDateTime date;
        private EventTimeSpan time;
        private EventType eventType;
        private Manager assignedManager;
        private List<Service> services;
        private Money totalCost;
        private Money downPayment;

        public Client Client
        {
            get => client;
            set => client = value;
        }

        public EventDateTime Date
        {
            get => date;
            set => date = value;
        }

        public EventTimeSpan Time
        {
            get => time;
            set => time = value;
        }

        public EventType EventType
        {
            get => eventType;
            set => eventType = value;
        }

        public Manager AssignedManager
        {
            get => assignedManager;
            set => assignedManager = value;
        }

        public List<Service> Services
        {
            get => services;
            set => services = value;
        }

        public Money TotalCost
        {
            get => totalCost;
            set => totalCost = value;
        }

        public Money DownPayment
        {
            get => downPayment;
            set => downPayment = value;
        }

        private Event(Client client, EventDateTime date, EventTimeSpan time, EventType eventType, Manager assignedManager)
        {
            this.client = client;
            this.date = date;
            this.time = time;
            this.eventType = eventType;
            this.assignedManager = assignedManager;
            services = new List<Service>();
            CalculateTotalCost();
        }

        public static Event Make(Client client, DateTime date, TimeSpan startTime, TimeSpan endTime, EventType eventType, Manager assignedManager)
        {
            

            return new Event(client, EventDateTime.Make(date), EventTimeSpan.Make(startTime,endTime), eventType, assignedManager);
        }

        public void AddService(Service service)
        {
            services.Add(service);
            CalculateTotalCost();
        }

        public void RemoveService(Service service)
        {
            services.Remove(service);
            CalculateTotalCost();
        }

        public void ClearServices()
        {
            this.services = new List<Service>();
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            Money total = Money.Make(0);
            foreach (var service in services)
            {
                total = total + service.TotalCost;
            }
            this.totalCost = total;
            this.downPayment = this.totalCost * 0.35;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"ID: {Id}");
            sb.AppendLine($"Client: {client.Name.Value}");
            sb.AppendLine($"Date: {date.Value.Date.ToShortDateString()} from {time.StartTime} to {time.EndTime}");
            sb.AppendLine($"Type: {eventType}");
            sb.AppendLine($"Manager: {assignedManager.Name.Value}");
            sb.AppendLine("Services:");
            foreach (var service in Services)
            {
                sb.AppendLine(service.ToString());
            }
            sb.AppendLine($"Total Cost: {totalCost.ToString()}");
            sb.AppendLine($"Down payment: {downPayment.ToString()}");
            return sb.ToString();
        }
    }
}
