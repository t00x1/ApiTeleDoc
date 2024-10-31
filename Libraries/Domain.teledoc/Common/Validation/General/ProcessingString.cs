using Domain.Interfaces.Validation ;

namespace Domain.General.Validation 
{
    public class ProcessingString : IProcessingString
    {
        public bool ClearingString(ref string value) 
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            
            value = value.Trim();
            return true; 
        }
    }
}