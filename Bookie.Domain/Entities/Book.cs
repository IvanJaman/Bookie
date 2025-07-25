using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Blurb { get; set; }
        public int? PublicationYear { get; set; }
        public int? PageCount { get; set; }
        public string CoverPhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal AverageRating { get; set; }
        public string GetBookUrl { get; set; }

        public Guid? GenreId { get; set; }
        public Genre Genre { get; set; }
        public Guid? LanguageId { get; set; }
        public Language Language { get; set; }
        public Guid? PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ShelfBook> ShelfBooks { get; set; } = new List<ShelfBook>();
    }
}
