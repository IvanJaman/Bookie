using System.ComponentModel.DataAnnotations;

namespace Bookie.Application.DTOs
{
    public class GenreDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
