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
    public class ClientValidation : GeneralValidation, IClientValidation
    {
        private bool CheckType<EnEl>(int? index)
        {
            if(index == null){
                return false;
            }
            var values = Enum.GetValues(typeof(EnEl)).Cast<int>();
            return values.Contains((int)index);
        }

        public bool Validation(ClientDto entity, List<string> errors)
        {
            bool isValid = true;

            isValid &= ValidateINN(entity.INN, errors);
            isValid &= ValidateType(entity.Type, errors);
            isValid &= ValidateStatus(entity.Status, errors);
            isValid &= ValidatePhoneNumber(entity.Phone, errors);
            isValid &= ValidateEmail(entity.Email, errors);

            return isValid;
        }

        

        public bool ValidateType(int? type, List<string> errors)
        {
            if (!CheckType<ClientType>(type))
            {
                errors.Add("Invalid client type");
                return false;
            }return true;
        }

        public bool ValidateStatus(int? status, List<string> errors)
        {
            if (!CheckType<ClientStatus>(status))
            {
                errors.Add("Invalid client status");
                return false;
            }return true;
        }

        
    }
}