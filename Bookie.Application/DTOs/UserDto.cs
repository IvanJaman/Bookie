using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string? WebsiteUrl { get; set; }
        public List<ShelfDto> Shelves { get; set; } = new();
    }
}
