using EventManagementSystem.Application.ClientServices;
using EventManagementSystem.Application.EmployeeServices;
using EventManagementSystem.Application.EventServices;
using EventManagementSystem.Contracts;
using EventManagementSystem.Domain.ClientEntity;
using EventManagementSystem.Domain.EmployeeEntity;
using EventManagementSystem.Domain.EventAggregate;
using EventManagementSystem.Domain.EventAggregate.ValueObjects;
using EventManagementSystem.Domain.ManagerEntity;
using EventManagementSystem.Domain.ServiceEntity.ValueObjects;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementSystem.Presentation.ConsoleApp.EventControllers
{
    public class EventController
    {
        private readonly EventService eventService;
        private readonly ClientService clientService;
        private readonly EmployeeService employeeService;

        public EventController(EventService eventService, ClientService clientService, EmployeeService employeeService)
        {
            this.eventService = eventService;
            this.clientService = clientService;
            this.employeeService = employeeService;
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Event Management ===");
            Console.WriteLine("1. Show Events");
            Console.WriteLine("2. Create Event");
            Console.WriteLine("3. Update Event");
            Console.WriteLine("4. Cancel Event");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowEvents();
                    break;
                case "2":
                    CreateEvent();
                    break;
                case "3":
                    UpdateEvent();
                    break;
                case "4":
                    CancelEvent();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void ShowEvents()
        {
            Console.Clear();
            Console.WriteLine("=== Events ===");

            var events = eventService.GetEvents();
            if (!events.Any())
            {
                Console.WriteLine("No events found.");
            }
            else
            {
                foreach (var evt in events)
                {
                    Console.WriteLine(evt.ToString());
                    Console.WriteLine("-------------------------------------------------------");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void CreateEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Create New Event ===");

            string isNewClient;
            HelperFunctions.PropertyValidation(out isNewClient, value => value.ToLower() == "y" || value.ToLower() == "n",
                "Add a new client? y/n: ", "Invalid option. Select y/n");

            Client client = null;
            // Get client information
            if (isNewClient.ToLower() == "y")
            {

                string name;
                HelperFunctions.PropertyValidation(out name, value => Name.Validate(value),
                    "Name: ", "Client name cannot be empty. Please try again.");

                string dni;
                HelperFunctions.PropertyValidation(out dni, value => DNI.Validate(value),
                    "DNI: ", "Invalid client DNI format. Please try again.");

                var clientRequest = new ClientRequest
                {
                    Name = name,
                    DNI = dni
                };

                client = clientService.AddClient(clientRequest);
                Console.WriteLine("Client added successfully!");
            }
            else
            {
                var clients = clientService.GetClients().ToList();
                if (!clients.Any())
                {
                    Console.WriteLine("No clients available.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }
                HelperFunctions.SelectItemFromList(out client, clients, "\nAvailable clients:", "Select client: ");
            }

            // Get event details
            EventDateTime date;
            do
            {
                HelperFunctions.PropertyValidation(out date, "Date (dd/mm/yyyy): ", "Invalid date. Please use dd/mm/yyyy and a non past date.");
                if (!eventService.IsDateAvailable(date))
                {
                    Console.WriteLine("The date is not available, please enter another one.");
                }
            }
            while (!eventService.IsDateAvailable(date));


            EventTimeSpan time;
            HelperFunctions.PropertyValidation(out time, "Start time and end time (hh:mm): ", "Invalid time. Please use hh:mm.");

            // Display available event types
            EventType eventType;
            var eventTypes = Enum.GetValues(typeof(EventType)).Cast<EventType>().ToList();
            HelperFunctions.SelectItemFromList(out eventType, eventTypes, "\nAvailable event types:", "Select event type: ");

            // Select manager
            Manager manager = null;

            string assingNewManager;
            HelperFunctions.PropertyValidation(out assingNewManager, value => value.ToLower() == "y" || value.ToLower() == "n",
                "Assing a new manager? y/n: ", "Invalid option. Select y/n");
            if (assingNewManager.ToLower() == "y")
            {
                var employees = employeeService.GetEmployeesByRol("employee");
                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees available to assign to the event.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }
                Employee employee;
                HelperFunctions.SelectItemFromList(out employee, employees, "\nAvailable employees:", "Select employee:");

                double bonus;
                HelperFunctions.PropertyValidation(out bonus, value => value != default,
                        "Bonus: ", "Invalid manager bonus. Please try again.");

                manager = employee.AssignAsManager(bonus);
                employeeService.UpdateEmployee(manager);
            }
            else
            {
                List<Manager> managers = employeeService.GetEmployeesByRol("manager").OfType<Manager>().ToList();
                if (managers.Count == 0)
                {
                    Console.WriteLine("No managers available to assign to the event.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }
                HelperFunctions.SelectItemFromList(out manager, managers, "\nAvailable managers:", "Select manager:");
            }


            // Create services
            var services = new List<ServiceRequest>();
            while (true)
            {
                Console.WriteLine("\nAdd a service? y/n: ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    if (services.Count != 0) break;
                    Console.WriteLine("Must add at least 1 service");
                }

                ServiceType serviceType;
                var serviceTypes = Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();
                HelperFunctions.SelectItemFromList(out serviceType, serviceTypes, "Available service types:", "Select service:");

                string description;
                HelperFunctions.PropertyValidation(out description, value => !string.IsNullOrWhiteSpace(value), "Description: ", "Description cannot be empty. Please try again.");

                int quantity;
                HelperFunctions.PropertyValidation(out quantity, value => value > 0, "Quantity: ", "Invalid quantity. Please enter a positive integer.");

                double unitCost;
                HelperFunctions.PropertyValidation(out unitCost, value => value >= 0, "Unit cost: ", "Invalid unit cost. Please enter a positive number.");

                services.Add(new ServiceRequest
                {
                    ServiceType = serviceType,
                    Description = description,
                    Quantity = quantity,
                    UnitCost = unitCost
                });
            }

            // Create event request
            var eventRequest = new EventRequest
            {
                Client = client,
                Date = date.Value,
                StartTime = time.StartTime,
                EndTime = time.EndTime,
                EventType = eventType,
                AssignedManagerId = manager.Id,
                Services = services
            };

            eventService.CreateEvent(eventRequest);
            Console.WriteLine("\nEvent created successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void UpdateEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Update Event ===");

            var events = eventService.GetEvents();
            if (!events.Any())
            {
                Console.WriteLine("No events available to update.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            Event selectedEvent;
            HelperFunctions.SelectItemFromList(out selectedEvent, events, "\nAvailable events:", "Select event to update:");

            Console.WriteLine("\nUpdating details for the selected event.");

            // Update date
            EventDateTime newDate;
            HelperFunctions.PropertyValidation(out newDate, "New Date (dd/mm/yyyy): ", "Invalid date. Please use dd/mm/yyyy.");
            if (!eventService.IsDateAvailable(newDate))
            {
                Console.WriteLine("The date is not available, keeping original date.");
                newDate = selectedEvent.Date;
            }

            // Update time
            EventTimeSpan newTime;
            HelperFunctions.PropertyValidation(out newTime, "New Start time and end time (hh:mm): ", "Invalid time. Please use hh:mm.");

            // Update assigned manager
            Manager newManager;
            List<Manager> managers = employeeService.GetEmployeesByRol("manager").OfType<Manager>().ToList();
            HelperFunctions.SelectItemFromList(out newManager, managers, "\nAvailable managers:", "Select new manager:");

            // Update services
            List<ServiceRequest> newServices = new List<ServiceRequest>();
            while (true)
            {
                Console.WriteLine("\nAdd a service? y/n: ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    if (newServices.Count != 0) break;
                    Console.WriteLine("Must add at least 1 service");
                }

                ServiceType serviceType;
                var serviceTypes = Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();
                HelperFunctions.SelectItemFromList(out serviceType, serviceTypes, "Available service types:", "Select service:");

                string description;
                HelperFunctions.PropertyValidation(out description, value => !string.IsNullOrWhiteSpace(value), "Description: ", "Description cannot be empty. Please try again.");

                int quantity;
                HelperFunctions.PropertyValidation(out quantity, value => value > 0, "Quantity: ", "Invalid quantity. Please enter a positive integer.");

                double unitCost;
                HelperFunctions.PropertyValidation(out unitCost, value => value >= 0, "Unit cost: ", "Invalid unit cost. Please enter a positive number.");

                newServices.Add(new ServiceRequest
                {
                    ServiceType = serviceType,
                    Description = description,
                    Quantity = quantity,
                    UnitCost = unitCost
                });
            }

            var updateRequest = new EventRequest
            {
                Date = newDate.Value,
                StartTime = newTime.StartTime,
                EndTime = newTime.EndTime,
                AssignedManagerId = newManager.Id,
                Services = newServices
            };

            eventService.UpdateEvent(selectedEvent.Id, updateRequest);
            Console.WriteLine("\nEvent updated successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void CancelEvent()
        {
            Console.Clear();
            Console.WriteLine("=== Cancel Event ===");

            var events = eventService.GetEvents();
            if (!events.Any())
            {
                Console.WriteLine("No events available to cancel.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            Event selectedEvent;
            HelperFunctions.SelectItemFromList(out selectedEvent, events, "\nAvailable events:", "Select event to cancel:");

            eventService.CancelEvent(selectedEvent.Id);
            Console.WriteLine("\nEvent canceled successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
