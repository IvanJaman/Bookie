using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        public string ISBN { get; set; }

        public string Blurb { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Publication year must be a valid year.")]
        public int PublicationYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page count must be greater than 0.")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        public string Language { get; set; }

        [Url(ErrorMessage = "Cover photo URL must be a valid URL.")]
        public string CoverPhotoUrl { get; set; }

        [Url(ErrorMessage = "Get book URL must be a valid URL.")]
        public string GetBookUrl { get; set; }

        [Required(ErrorMessage = "Genre name is required.")]
        public string GenreName { get; set; }
    }
}
