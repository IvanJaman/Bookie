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

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

            return await query.ToListAsync();
        }
    }
}
