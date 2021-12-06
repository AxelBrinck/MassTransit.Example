using System.ComponentModel.DataAnnotations;

namespace MassTransit.Example.Authentication.Models.Dto.Incoming
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}