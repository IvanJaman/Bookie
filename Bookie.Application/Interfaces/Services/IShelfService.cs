using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IShelfService
    {
        Task<ShelfDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ShelfDto>> GetByUserIdAsync(Guid userId);
        Task<ShelfDto> CreateShelfAsync(CreateShelfDto createDto, Guid userId);
        Task<bool> RenameShelfAsync(Guid shelfId, string newName);
        Task<bool> DeleteShelfAsync(Guid shelfId);
        Task<bool> AddBookToShelfAsync(Guid shelfId, Guid bookId);
        Task<bool> RemoveBookFromShelfAsync(Guid shelfId, Guid bookId);
    }
}
