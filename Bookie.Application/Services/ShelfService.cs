using AutoMapper;
using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Bookie.Domain.Entities;

namespace Bookie.Application.Services
{
    public class ShelfService : IShelfService
    {
        private readonly IShelfRepository _shelfRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public ShelfService(IShelfRepository shelfRepo, IBookRepository bookRepo, IUserRepository userRepo, IMapper mapper)
        {
            _shelfRepo = shelfRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ShelfDto> GetByIdAsync(Guid id)
        {
            var shelf = await _shelfRepo.GetByIdAsync(id);
            if (shelf == null) throw new Exception("Shelf not found.");
            return _mapper.Map<ShelfDto>(shelf);
        }

        public async Task<IEnumerable<ShelfDto>> GetByUserIdAsync(Guid userId)
        {
            var shelves = await _shelfRepo.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ShelfDto>>(shelves);
        }

        public async Task<ShelfDto> CreateShelfAsync(CreateShelfDto createDto, Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var shelf = new Shelf
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                UserId = userId
            };

            await _shelfRepo.AddAsync(shelf);
            return _mapper.Map<ShelfDto>(shelf);
        }

        public async Task<bool> RenameShelfAsync(Guid shelfId, string newName)
        {
            var shelf = await _shelfRepo.GetByIdAsync(shelfId);
            if (shelf == null) return false;

            shelf.Name = newName;
            await _shelfRepo.UpdateAsync(shelf);
            return true;
        }

        public async Task<bool> DeleteShelfAsync(Guid shelfId)
        {
            var shelf = await _shelfRepo.GetByIdAsync(shelfId);
            if (shelf == null) return false;

            await _shelfRepo.DeleteAsync(shelfId);
            return true;
        }

        public async Task<bool> AddBookToShelfAsync(Guid shelfId, Guid bookId)
        {
            var shelf = await _shelfRepo.GetByIdAsync(shelfId);
            if (shelf == null) throw new Exception("Shelf not found.");

            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book == null) throw new Exception("Book not found.");

            if (shelf.ShelfBooks.Any(sb => sb.BookId == bookId))
                throw new Exception("Book is already in this shelf.");

            shelf.ShelfBooks.Add(new ShelfBook
            {
                ShelfId = shelfId,
                BookId = bookId,
                AddedAt = DateTime.UtcNow
            });

            await _shelfRepo.UpdateAsync(shelf);
            return true;
        }

        public async Task<bool> RemoveBookFromShelfAsync(Guid shelfId, Guid bookId)
        {
            var shelf = await _shelfRepo.GetByIdAsync(shelfId);
            if (shelf == null) throw new Exception("Shelf not found.");

            var shelfBook = shelf.ShelfBooks.FirstOrDefault(sb => sb.BookId == bookId);
            if (shelfBook == null) return false;

            shelf.ShelfBooks.Remove(shelfBook);
            await _shelfRepo.UpdateAsync(shelf);
            return true;
        }
    }
}
