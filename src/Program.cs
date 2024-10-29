using EventManagementSystem.Application.ClientServices;
using EventManagementSystem.Application.EmployeeServices;
using EventManagementSystem.Application.EventServices;
using EventManagementSystem.Domain.HallEntity;
using EventManagementSystem.Domain.Repositories;
using EventManagementSystem.Infrastructure.Repositories;
using EventManagementSystem.Presentation.ConsoleApp;
using EventManagementSystem.Presentation.ConsoleApp.ClientControllers;
using EventManagementSystem.Presentation.ConsoleApp.EmployeeControllers;
using EventManagementSystem.Presentation.ConsoleApp.EventControllers;
using System;

namespace EventManagementSystem
{
	class Program
	{
		private static Hall hall;

		private static IClientRepository clientRepository;
		private static IEmployeeRepository employeeRepository;
		private static IEventRepository eventRepository;

		private static ClientService clientService;
		private static EmployeeService employeeService;
		private static EventService eventService;

		private static ClientController clientController;
		private static EmployeeController employeeController;
		private static EventController eventController;

		private static ConsoleMenu consoleMenu;
		public static void Main(string[] args)
		{
			hall = Hall.Make("Gianluca Events","Street 123");

            clientRepository = new ClientRepository();
			employeeRepository = new EmployeeRepository();
			eventRepository = new EventRepository();

			clientService = new ClientService(clientRepository);
			employeeService = new EmployeeService(employeeRepository, eventRepository);
			eventService = new EventService(eventRepository,employeeRepository,clientRepository, hall);

			clientController = new ClientController(clientService);
			employeeController = new EmployeeController(employeeService);
			eventController = new EventController(eventService,clientService,employeeService);

			consoleMenu = new ConsoleMenu(eventController, employeeController, clientController);
			consoleMenu.DisplayMainMenu();
        }
	}
}