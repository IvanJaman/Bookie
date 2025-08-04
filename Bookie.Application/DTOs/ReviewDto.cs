namespace Bookie.Application.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string BookTitle { get; set; }
        public Guid BookId { get; set; }
    }
}
