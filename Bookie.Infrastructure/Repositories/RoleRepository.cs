using Bookie.Domain.Entities;
using Bookie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookie.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookieDbContext _context;

        public RoleRepository(BookieDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(string roleName) 
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<Role?> GetByIdAsync(Guid id) 
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
