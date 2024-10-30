using System;
using System.Collections.Generic;
using Domain.ModelsDTO;

namespace Domain.Interfaces.Validation
{
    public interface IClientValidation
    {
        bool Validation(ClientDto entity, List<string> errors); 
       
      
      
    }
}