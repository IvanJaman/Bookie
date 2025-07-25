using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Domain.Entities
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public string WebsiteUrl { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
