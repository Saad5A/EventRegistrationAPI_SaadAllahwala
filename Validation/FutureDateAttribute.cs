using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_SaadAllahwala.Validation
{
    // Ensures that an event date is in the future.
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }

            return false;
        }
    }
}