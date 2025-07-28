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

        modelBuilder.Entity<ShelfBook>()
            .HasKey(sb => new { sb.ShelfId, sb.BookId }); 
    }
}
