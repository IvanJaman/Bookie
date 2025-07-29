using Bookie.Application.Interfaces;
using Bookie.Domain.Entities;
using Bookie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookie.Infrastructure.Repositories
{
    public class ShelfBookRepository : IShelfBookRepository
    {
        private readonly BookieDbContext _context;

        public ShelfBookRepository(BookieDbContext context)
        {
            _context = context;
        }

        public async Task AddBookToShelfAsync(Guid shelfId, Guid bookId, DateTime addedAt)
        {
            var shelfBook = new ShelfBook
            {
                ShelfId = shelfId,
                BookId = bookId,
                AddedAt = addedAt
            };

            await _context.ShelfBooks.AddAsync(shelfBook);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookFromShelfAsync(Guid shelfId, Guid bookId)
        {
            var shelfBook = await _context.ShelfBooks
                .FirstOrDefaultAsync(sb => sb.ShelfId == shelfId && sb.BookId == bookId);

            if (shelfBook != null)
            {
                _context.ShelfBooks.Remove(shelfBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShelfBook>> GetBooksInShelfAsync(Guid shelfId)
        {
            return await _context.ShelfBooks
                .Where(sb => sb.ShelfId == shelfId)
                .Include(sb => sb.Book)
                .ToListAsync();
        }
    }
}
