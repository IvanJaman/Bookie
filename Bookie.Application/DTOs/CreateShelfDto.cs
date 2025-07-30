using System.ComponentModel.DataAnnotations;

namespace Bookie.Application.DTOs
{
    public class CreateShelfDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Shelf name cannot be longer than 100 characters.")]
        public string Name { get; set; }
    }
}
