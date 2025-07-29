using Bookie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookie.Application.Interfaces
{
    public interface IShelfRepository
    {
        Task<Shelf?> GetByIdAsync(Guid id);
        Task<IEnumerable<Shelf>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Shelf shelf);
        Task UpdateAsync(Shelf shelf);
        Task DeleteAsync(Guid id);
    }
}
