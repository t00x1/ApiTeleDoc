using Domain.Interfaces.Services;
using Domain.Response; 
using Domain.Models;
using Domain.Interfaces;
using Domain.Common;
using System;
using Domain.ModelsDTO;
using Microsoft.Extensions.Logging;



namespace Business.Services
{
    public class FounderServiceContainer : IFounderServiceContainer
    {
        private readonly IFounderServiceCreate _founderServiceCreate;
        private readonly IFounderServiceUpdate _founderServiceUpload;
        private readonly IFounderServiceDelete _founderServiceDelete;
        private readonly IFounderServiceRead _founderServiceRead;
        public FounderServiceContainer(IFounderServiceCreate founderServiceCreate, IFounderServiceUpdate founderServiceUpdate, IFounderServiceRead founderServiceRead, IFounderServiceDelete founderServiceDelete)
        {
            _founderServiceCreate = founderServiceCreate;
            _founderServiceUpload = founderServiceUpdate;
            _founderServiceRead = founderServiceRead;
            _founderServiceDelete = founderServiceDelete;

        }
        public IFounderServiceRead FounderServiceRead => _founderServiceRead;
        public IFounderServiceCreate FounderServiceCreate => _founderServiceCreate;
        public IFounderServiceUpdate FounderServiceUpdate => _founderServiceUpload;
        public IFounderServiceDelete FounderServiceDelete => _founderServiceDelete;
    }
}