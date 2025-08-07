namespace Bookie.Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Blurb { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string CoverPhotoUrl { get; set; }
        public double AverageRating { get; set; }
        public string GenreName { get; set; }
        public string LanguageName { get; set; }
        public string CreatedByUsername { get; set; }
    }
}
