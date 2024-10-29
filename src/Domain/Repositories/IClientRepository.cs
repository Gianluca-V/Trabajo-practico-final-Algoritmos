using EventManagementSystem.Domain.ClientEntity;
using System;
using System.Collections.Generic;

namespace EventManagementSystem.Domain.Repositories
{
    public interface IClientRepository
    {
        void AddClient(Client client);
        Client GetClientByDNI(string dni);
        Client GetClientById(Guid id);
        List<Client> GetClients();
        void UpdateClient(Client client);
        
    }
}
