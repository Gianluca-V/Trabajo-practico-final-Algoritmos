using EventManagementSystem.Application.ClientServices;
using EventManagementSystem.Contracts;
using EventManagementSystem.Domain.ClientEntity;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Linq;

namespace EventManagementSystem.Presentation.ConsoleApp.ClientControllers
{
    public class ClientController
    {
        private readonly ClientService clientService;

        public ClientController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Client Management ===");
            Console.WriteLine("1. Show Clients");
            Console.WriteLine("2. Add Client");
            Console.WriteLine("3. Update Client");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowClients();
                    break;
                case "2":
                    AddClient();
                    break;
                case "3":
                    UpdateClient();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
        private void ShowClients()
        {
            Console.Clear();
            Console.WriteLine("=== Clients ===");

            var clients = clientService.GetClients();
            if (!clients.Any())
            {
                Console.WriteLine("No clients found.");
            }
            else
            {
                foreach (var client in clients)
                {
                    Console.WriteLine(client.ToString());
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void AddClient()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Client ===");

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

            clientService.AddClient(clientRequest);
            Console.WriteLine("Client added successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void UpdateClient()
        {
            Console.Clear();
            Console.WriteLine("=== Update Client ===");
            // Show current clients
            var clients = clientService.GetClients().ToList();
            if (!clients.Any())
            {
                Console.WriteLine("No clients available to update.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Select client to update
            Client clientToUpdate;
            HelperFunctions.SelectItemFromList(out clientToUpdate, clients, "\nAvailable clients:", "Select client to update: ");

            // Get new values
            string newName;
            HelperFunctions.PropertyValidation(out newName, value => Name.Validate(value),
                $"New Name: ", "Client name cannot be empty. Please try again.");

            string newDNI;
            HelperFunctions.PropertyValidation(out newDNI, value => DNI.Validate(value),
                $"New DNI: ", "Invalid client DNI format. Please try again.");

            // Update client information
            var clientRequest = new ClientRequest
            {
                Name = newName,
                DNI = newDNI
            };

            clientService.UpdateClient(clientToUpdate.Id, clientRequest);
            Console.WriteLine("Client updated successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
