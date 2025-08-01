using System.ComponentModel.DataAnnotations;

namespace Bookie.Application.DTOs
{
    public class UpdateBookDto
    {
        public string? Author { get; set; }

        public string? Title { get; set; }

        public string? ISBN { get; set; }

        public string? Blurb { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Publication year must be a valid year.")]
        public int? PublicationYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page count must be greater than 0.")]
        public int? PageCount { get; set; }

        public string? Language { get; set; }

        [Url(ErrorMessage = "Cover photo URL must be a valid URL.")]
        public string? CoverPhotoUrl { get; set; }

        [Url(ErrorMessage = "Get book URL must be a valid URL.")]
        public string? GetBookUrl { get; set; }
        public string? GenreName { get; set; }
    }
}
