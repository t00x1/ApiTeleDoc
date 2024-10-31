using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Enums;
using Domain.ModelsDTO;
using Domain.Interfaces.Validation;
using Domain.General.Validation;


namespace Domain.Validation.CertainValidation
{
    public class FounderValidation : GeneralValidation, IFounderValidation
    {
        public bool Validation(FounderDto entity, List<string> errors)
        {
            bool isValid = true;

            isValid &= ValidateINN(entity.INN, errors);
            isValid &= ValidatePhoneNumber(entity.Phone, errors);
            isValid &= ValidateEmail(entity.Email, errors);

            return isValid;
        }

    }
}