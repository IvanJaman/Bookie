using Bookie.Application.Interfaces;
using Bookie.Domain.Entities;
using Bookie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookie.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookieDbContext _context;

        public GenreRepository(BookieDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<Genre?> GetByNameAsync(string name)
        {
            return await _context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
        }
    }
}
