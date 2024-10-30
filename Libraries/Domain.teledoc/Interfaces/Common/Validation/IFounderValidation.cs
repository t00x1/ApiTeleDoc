using System;
using System.Collections.Generic;
using Domain.ModelsDTO;

namespace Domain.Interfaces.Validation
{
    public interface IFounderValidation
    {
        public bool Validation(FounderDto entity, List<string> errors);
        
    }
}