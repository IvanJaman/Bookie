using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Expires { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}
