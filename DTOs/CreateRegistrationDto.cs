using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_SaadAllahwala.DTOs
{
    // Receives and validates registration information.
    public class CreateRegistrationDto
    {
        [Required(ErrorMessage = "Student name is required.")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
    }
}