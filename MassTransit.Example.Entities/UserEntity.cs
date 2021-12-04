using System.ComponentModel.DataAnnotations;

namespace MassTransit.Example.Entities
{
    public class UserEntity : EntityBase
    {
        [Key]
        [Required]
        public string? Username { get; set; }
    }
}