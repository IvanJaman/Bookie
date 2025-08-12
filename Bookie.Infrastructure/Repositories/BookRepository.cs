using Bookie.Application.Interfaces;
using Bookie.Domain.Entities;
using Bookie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookie.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookieDbContext _context;

        public BookRepository(BookieDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.CreatedByUser)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetRecentlyAddedByGenreAsync(Genre genre, int count)
        {
            return await _context.Books
                .Where(b => b.Genre.Id == genre.Id)
                .OrderByDescending(b => b.CreatedAt) 
                .Take(count)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByGenreAsync(Genre genre)
        {
            return await _context.Books
                .Where(b => b.Genre.Id == genre.Id)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchAsync(string? title, string? author)
        {
            var query = _context.Books
                .Include(b => b.Genre)
                .AsQueryable();

            title = title?.Trim();
            author = author?.Trim();

            if (!string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b =>
                    (!string.IsNullOrWhiteSpace(title) &&
                     b.Title.ToLower().Contains(title.ToLower())) ||
                    (!string.IsNullOrWhiteSpace(author) &&
                     b.Author.ToLower().Contains(author.ToLower()))
                );
            }

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Book>> SearchInShelfAsync(Guid shelfId, string? title, string? author)
        {
            var query = _context.ShelfBooks
                .Where(sb => sb.ShelfId == shelfId)
                .Include(sb => sb.Book)
                .ThenInclude(b => b.Genre)
                .Select(sb => sb.Book)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                var lowerTitle = title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(lowerTitle));
            }

            if (!string.IsNullOrEmpty(author))
            {
                var lowerAuthor = author.ToLower();
                query = query.Where(b => b.Author.ToLower().Contains(lowerAuthor));
            }

            return await query.ToListAsync();
        }
    }
}
