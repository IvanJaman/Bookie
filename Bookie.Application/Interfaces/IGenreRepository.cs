using Bookie.Domain.Entities;

namespace Bookie.Application.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByNameAsync(string name);
    }
}
