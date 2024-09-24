using API.Data.Converter.Implementations;
using API.Data.VO;
using API.Repositories.Client;
using API.Repositories.Generic;
using Models;
using System.Linq;
using System.Net;

namespace API.Business.Implementations
{
    public class ClientBusinessImplementation : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository<AddressModel> _addressRepository;
        private readonly ClientConverter _clientConverter;
        private readonly AddressConverter _addressConverter;
        public ClientBusinessImplementation(IClientRepository clientRepository,IRepository<AddressModel> addressRepository, ClientConverter clientConverter, AddressConverter addressConverter)
        {
            _clientRepository = clientRepository;
            _addressRepository = addressRepository;
            _clientConverter = clientConverter;
            _addressConverter = addressConverter;
        }

        public ClientVO Create(ClientVO client)
        {
            var clientEntity = _clientConverter.Parse(client);
            clientEntity = _clientRepository.Create(clientEntity);
            return _clientConverter.Parse(clientEntity);
        }

        public List<ClientVO> FindAll()
        {
            return _clientConverter.Parse(_clientRepository.FindAll());
        }

        public ClientVO FindByEmail(string email)
        {
            return _clientConverter.Parse(_clientRepository.FindByEmail(email));
        }

        public ClientVO Update(ClientVO client)
        {
            var clientEntity = _clientConverter.Parse(client);
            clientEntity = _clientRepository.Update(clientEntity);
            return _clientConverter.Parse(_clientRepository.FindByEmail(clientEntity.Email));
        }

        public ClientVO AddAddress(ClientVO client, AddressVO address)
        {
            if (client == null || address == null) return null;
            var addressEntity = _addressConverter.Parse(address);
            var clientEntity = _clientConverter.Parse(client);

            addressEntity.ClientId = clientEntity.Id;

            clientEntity.Addresses.Add(_addressRepository.Create(addressEntity));

            return _clientConverter.Parse(clientEntity);
        }

        public ClientVO RemoveAddress(ClientVO client, int addressID)
        {
            if (client == null) return null;
            var addressEntity = _addressRepository.FindByID(addressID);
            if (addressEntity == null) return null;

            if (addressEntity.ClientId != client.Id) return null;
            _addressRepository.Delete(addressEntity.Id);

            return _clientConverter.Parse(_clientRepository.FindByID(client.Id));
        }

        public ClientVO UpdateAddress(ClientVO client, AddressVO address)
        {
            if (client == null || address == null ) return null;
            var clientSearch = FindByEmail(client.Email);
            var addressEntity = _addressConverter.Parse(address);
            addressEntity.ClientId = clientSearch.Id;

            if (addressEntity == null) return null;

            if (clientSearch.Addresses.FirstOrDefault(e => e.Id == addressEntity.Id) == null) return null;
            _addressRepository.Update(addressEntity);

            return FindByEmail(client.Email);

        }
    }
}
