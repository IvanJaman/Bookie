using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Domain.Entities
{
    public class ShelfBook
    {
        public Guid ShelfId { get; set; }
        public Shelf Shelf { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
