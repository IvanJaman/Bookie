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
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookieDbContext _context;

        public ReviewRepository(BookieDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
