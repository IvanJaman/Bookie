using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class CreateBookDto
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public string Blurb { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public string Language { get; set; }

        public string CoverPhotoUrl { get; set; }

        public string GetBookUrl { get; set; }

        [Required]
        public string GenreName { get; set; }
    }
}
