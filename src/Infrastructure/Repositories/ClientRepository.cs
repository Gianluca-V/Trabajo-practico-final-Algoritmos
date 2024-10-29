using EventManagementSystem.Domain.ClientEntity;
using EventManagementSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using EventManagementSystem.Domain.ClientEntity.Exceptions;

namespace EventManagementSystem.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> clients = new List<Client>();

        public void AddClient(Client client) => clients.Add(client);
        public Client GetClientByDNI(string dni) => clients.FirstOrDefault(c => c.Dni.ToString() == dni);
        public Client GetClientById(Guid id) => clients.FirstOrDefault(e => e.Id == id);
        public List<Client> GetClients() => clients.ToList();

        public void UpdateClient(Client client)
        {
            if (GetClientById(client.Id) == null)
            {
                throw new ClientDoesNotExistException("A client with given ID does not exist");
            }

            RemoveClient(client);
            AddClient(client);
        }

        public void RemoveClient(Client client)
        {
            this.clients.Remove(client);
        }
    }
}
