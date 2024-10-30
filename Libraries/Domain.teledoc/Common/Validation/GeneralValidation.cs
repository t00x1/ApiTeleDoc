using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Interfaces.Validation;

namespace Domain.Validation
{
    public abstract class GeneralValidation : IGeneralValidation
    {
        private const int INNLength = 10; 
        private const int PhoneNumberLength = 10; 
        private const string PhoneNumberPattern = @"^\d{10}$"; 
        private const string NumericPattern = @"^\d+$"; 

        public bool ValidateINN(string? inn, List<string> errors)
        {
            if (!ValidateINN(inn))
            {
                errors.Add("Invalid INN");
                return false;
            }return true;
        }
        public bool ValidatePhoneNumber(string? phone, List<string> errors)
        {
            if (!ValidatePhoneNumber(phone))
            {
                errors.Add("Invalid phone number");
                return false;
            }return true;
        }

        public bool ValidateEmail(string? email, List<string> errors)
        {
            if (!ValidateEmail(email))
            {
                errors.Add("Invalid Email");
                 return false;
            }
            return true;
        }

        private bool ValidateINN(string? inn)
        {
            if (string.IsNullOrEmpty(inn) || !IsNumeric(inn) || inn.Length != INNLength)
            {
                return false;
            }
            return true;
        }

        private bool IsNumeric(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return Regex.IsMatch(value, NumericPattern);
        }
        

        private bool ValidatePhoneNumber(string? phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }
            var regex = new Regex(PhoneNumberPattern);
            return regex.IsMatch(phone);
        }

        private bool ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
