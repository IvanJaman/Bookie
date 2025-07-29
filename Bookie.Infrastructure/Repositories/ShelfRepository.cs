using Bookie.Application.Interfaces;
using Bookie.Domain.Entities;
using Bookie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Infrastructure.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly BookieDbContext _context;

        public ShelfRepository(BookieDbContext context)
        {
            _context = context;
        }

        public async Task<Shelf?> GetByIdAsync(Guid id)
        {
            return await _context.Shelves
                .Include(s => s.ShelfBooks)
                .ThenInclude(sb => sb.Book)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Shelf>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Shelves
                .Where(s => s.UserId == userId)
                .Include(s => s.ShelfBooks)
                .ThenInclude(sb => sb.Book)
                .ToListAsync();
        }

        public async Task AddAsync(Shelf shelf)
        {
            await _context.Shelves.AddAsync(shelf);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shelf shelf)
        {
            _context.Shelves.Update(shelf);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf != null)
            {
                _context.Shelves.Remove(shelf);
                await _context.SaveChangesAsync();
            }
        }
    }
}
