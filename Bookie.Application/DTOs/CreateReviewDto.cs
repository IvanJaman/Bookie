using System;
using System.ComponentModel.DataAnnotations;

namespace Bookie.Application.DTOs
{
    public class CreateReviewDto
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Review text can't exceed 1000 characters.")]
        public string? Text { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
