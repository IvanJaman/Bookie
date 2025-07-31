using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string? WebsiteUrl { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }

}
