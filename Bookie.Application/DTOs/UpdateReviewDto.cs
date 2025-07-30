using System.ComponentModel.DataAnnotations;

namespace Bookie.Application.DTOs
{
    public class UpdateReviewDto
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public string Text { get; set; }
    }
}
