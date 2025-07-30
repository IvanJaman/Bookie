using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class ShelfDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Shelf name is required.")]
        [StringLength(100, ErrorMessage = "Shelf name cannot exceed 100 characters.")]
        public string Name { get; set; }

        public List<ShelfBookDto> Books { get; set; } = new();
    }
}
