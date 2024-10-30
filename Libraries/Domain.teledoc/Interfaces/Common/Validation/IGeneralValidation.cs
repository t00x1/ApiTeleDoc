using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Interfaces.Validation 
{


    public interface IGeneralValidation
    {
         public bool ValidateINN(string inn, List<string> errors);
        public bool ValidatePhoneNumber(string phone, List<string> errors);
        public bool ValidateEmail(string email, List<string> errors);
    }
}


