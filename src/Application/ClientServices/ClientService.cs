using EventManagementSystem.Contracts;
using EventManagementSystem.Domain.ClientEntity;
using EventManagementSystem.Domain.Repositories;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using EventManagementSystem.Domain.ClientEntity.Exceptions;

namespace EventManagementSystem.Application.ClientServices
{
    public class ClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public Client AddClient(ClientRequest request)
        {
            var client = Client.Make(request.Name, request.DNI);
            clientRepository.AddClient(client);
            return client;
        }

        public Client GetClientByDNI(string dni) => clientRepository.GetClientByDNI(dni);
        public List<Client> GetClients() => clientRepository.GetClients();

        public void UpdateClient(Guid id, ClientRequest request)
        {
            Client client = clientRepository.GetClientById(id);
            if (client == null)
            {
                throw new ClientDoesNotExistException("A client with given ID does not exist");
            }
            client.Name = Name.Make(request.Name);
            client.Dni = DNI.Make(request.DNI);
            clientRepository.UpdateClient(client);
        }
    }
}
