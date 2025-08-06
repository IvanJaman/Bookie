using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookDto> GetByIdAsync(Guid id);
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author); 
        Task<BookDto> CreateAsync(CreateBookDto newBook, Guid createdByUserId);
        Task<bool> UpdateAsync(Guid id, UpdateBookDto updatedBook);
        Task<bool> DeleteAsync(Guid id);

        Task<Dictionary<string, IEnumerable<BookDto>>> GetRecentlyAddedByGenreAsync(int count);
        Task<IEnumerable<BookDto>> GetByGenreAsync(string genreName);
        Task<IEnumerable<BookDto>> SearchInShelfAsync(Guid shelfId, string? title, string? author);
    }
}
