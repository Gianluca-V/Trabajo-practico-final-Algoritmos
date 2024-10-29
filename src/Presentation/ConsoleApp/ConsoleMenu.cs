using EventManagementSystem.Application.Middleware;
using EventManagementSystem.Presentation.ConsoleApp.ClientControllers;
using EventManagementSystem.Presentation.ConsoleApp.EmployeeControllers;
using EventManagementSystem.Presentation.ConsoleApp.EventControllers;
using System;

namespace EventManagementSystem.Presentation.ConsoleApp
{
    public class ConsoleMenu
    {
        private readonly EventController eventController;
        private readonly EmployeeController employeeController;
        private readonly ClientController clientController;

        public ConsoleMenu(EventController eventController, EmployeeController employeeController, ClientController clientController)
        {
            this.eventController = eventController;
            this.employeeController = employeeController;
            this.clientController = clientController;
        }

        public void DisplayMainMenu()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Party Management System ===");
                    Console.WriteLine("1. Manage Events");
                    Console.WriteLine("2. Manage Employees");
                    Console.WriteLine("3. Manage Clients");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nSelect an option: ");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            eventController.ShowMenu();
                            break;
                        case "2":
                            employeeController.ShowMenu();
                            break;
                        case "3":
                            clientController.ShowMenu();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Middleware.HandleException(ex);
                DisplayMainMenu();
            }
        }
    }
}

