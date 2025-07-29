using Microsoft.EntityFrameworkCore;
using Bookie.Domain.Entities;

namespace Bookie.Infrastructure.Data;

public class BookieDbContext : DbContext
{
    public BookieDbContext(DbContextOptions<BookieDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<ShelfBook> ShelfBooks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite key for ShelfBook
        modelBuilder.Entity<ShelfBook>()
            .HasKey(sb => new { sb.ShelfId, sb.BookId });

        // seeding Roles
        var userRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var publisherRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var adminRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = userRoleId, Name = "User" },
            new Role { Id = publisherRoleId, Name = "Publisher" },
            new Role { Id = adminRoleId, Name = "Admin" }
        );

        // seeding Users - password for every user is "password1"
        var adminUserId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var normalUser1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var normalUser2Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var publisherUserId = Guid.Parse("77777777-7777-7777-7777-777777777777");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUserId,
                Username = "admin",
                Email = "admin@bookie.com",
                PasswordHash = "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi",
                Bio = "I am the admin",
                RoleId = adminRoleId
            },
            new User
            {
                Id = normalUser1Id,
                Username = "reader1",
                Email = "reader1@bookie.com",
                PasswordHash = "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi",
                Bio = "I love books",
                RoleId = userRoleId
            },
            new User
            {
                Id = normalUser2Id,
                Username = "reader2",
                Email = "reader2@bookie.com",
                PasswordHash = "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi",
                Bio = "Avid book collector",
                RoleId = userRoleId
            },
            new User
            {
                Id = publisherUserId,
                Username = "pub1",
                Email = "publisher@bookie.com",
                PasswordHash = "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi",
                Bio = "I publish books",
                RoleId = publisherRoleId
            }
        );

        // seeding Genres 
        var horrorGenreId = Guid.Parse("88888888-8888-8888-8888-888888888888");
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = horrorGenreId, Name = "Horror" }
        );

        // seeding Books
        var sampleBookId = Guid.Parse("99999999-9999-9999-9999-999999999999");
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = sampleBookId,
                Author = "Mary Shelley",
                Title = "Frankenstein",
                ISBN = "9780143131847",
                Blurb = "A classic tale of science and horror.",
                PublicationYear = 1818,
                PageCount = 280,
                CoverPhotoUrl = "https://ia600100.us.archive.org/view_archive.php?archive=/5/items/l_covers_0012/l_covers_0012_75.zip&file=0012752093-L.jpg",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                AverageRating = 4.5,
                GetBookUrl = "https://www.gutenberg.org/files/84/84-h/84-h.htm",
                GenreId = horrorGenreId,
                Language = "English",
                CreatedByUserId = publisherUserId
            }
        );

        // seeding Reviews
        var review1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var review2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                Id = review1Id,
                Rating = 5,
                Text = "An amazing read!",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UserId = normalUser1Id,
                BookId = sampleBookId
            },
            new Review
            {
                Id = review2Id,
                Rating = 4,
                Text = "Very well written, but a bit slow in the middle.",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UserId = normalUser2Id,
                BookId = sampleBookId
            }
        );
    }

}
