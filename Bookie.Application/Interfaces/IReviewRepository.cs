using Bookie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookie.Application.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByBookIdAsync(Guid bookId);
        Task<Review?> GetByIdAsync(Guid id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Guid id);
    }
}
