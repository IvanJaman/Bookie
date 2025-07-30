using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class CreateBookDto
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Blurb { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string GetBookUrl { get; set; }
        public string GenreName { get; set; }
    }
}
