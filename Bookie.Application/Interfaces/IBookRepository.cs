using Bookie.Domain.Entities;

namespace Bookie.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<Book>> GetRecentlyAddedByGenreAsync(Genre genre, int count); 
        Task<IEnumerable<Book>> GetByGenreAsync(Genre genre);
        Task<IEnumerable<Book>> SearchAsync(string? title, string? author);
        Task<IEnumerable<Book>> SearchInShelfAsync(Guid shelfId, string? title, string? author);
    }
}
