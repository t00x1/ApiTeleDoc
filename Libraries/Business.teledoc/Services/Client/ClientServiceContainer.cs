using Domain.Interfaces.Services;
using Domain.Response; 
using Domain.Models;
using Domain.Interfaces;
using Domain.Common;
using System;
using Domain.ModelsDTO;
using Microsoft.Extensions.Logging;
using Business.Interfaces.Services;

namespace Business.Services
{
    public class ClientServiceContainer : IClientServiceContainer
    {
        private readonly IClientServiceCreate _clientServiceCreate;
        private readonly IClientServiceUpdate _clientServiceUpdate;
        private readonly IClientServiceDelete _clientServiceDelete;
        private readonly IClientServiceRead _clientServiceRead;

        public ClientServiceContainer(IClientServiceCreate clientServiceCreate, IClientServiceUpdate clientServiceUpdate, IClientServiceDelete clientServiceDelete, IClientServiceRead clientServiceRead)
        {
            _clientServiceCreate = clientServiceCreate;
            _clientServiceUpdate = clientServiceUpdate;
            _clientServiceDelete = clientServiceDelete;
            _clientServiceRead = clientServiceRead;
        }

        public IClientServiceCreate ClientServiceCreate => _clientServiceCreate;
        public IClientServiceUpdate ClientServiceUpdate => _clientServiceUpdate;
        public IClientServiceDelete ClientServiceDelete => _clientServiceDelete;
        public IClientServiceRead ClientServiceRead => _clientServiceRead;
    }   
}
