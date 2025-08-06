using AutoMapper;
using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Bookie.Domain.Entities;

namespace Bookie.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepo, IMapper mapper, IGenreRepository genreRepo)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _genreRepo = genreRepo;
        }

        public async Task<BookDto> GetByIdAsync(Guid id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> SearchAsync(string? title, string? author)
        {
            var books = await _bookRepo.SearchAsync(title, author);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto newBook, Guid createdByUserId)
        {
            var genre = await _genreRepo.GetByNameAsync(newBook.GenreName);
            if (genre == null)
                throw new Exception($"Genre '{newBook.GenreName}' not found.");

            var book = _mapper.Map<Book>(newBook);
            book.Id = Guid.NewGuid();
            book.CreatedByUserId = createdByUserId;
            book.GenreId = genre.Id;

            await _bookRepo.AddAsync(book);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateBookDto updatedBook)
        {
            var existingBook = await _bookRepo.GetByIdAsync(id);
            if (existingBook == null) return false;
            _mapper.Map(updatedBook, existingBook);
            await _bookRepo.UpdateAsync(existingBook);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _bookRepo.DeleteAsync(id); 
            return true;
        }

        public async Task<Dictionary<string, IEnumerable<BookDto>>> GetRecentlyAddedByGenreAsync(int count)
        {
            var genres = await _genreRepo.GetAllAsync();
            var result = new Dictionary<string, IEnumerable<BookDto>>();

            foreach (var genre in genres)
            {
                var books = await _bookRepo.GetRecentlyAddedByGenreAsync(genre, count);
                result[genre.Name] = _mapper.Map<IEnumerable<BookDto>>(books);
            }

            return result;
        }

        public async Task<IEnumerable<BookDto>> GetByGenreAsync(string genreName)
        {
            var genre = await _genreRepo.GetByNameAsync(genreName);
            if (genre == null)
                throw new Exception($"Genre '{genreName}' not found.");

            var books = await _bookRepo.GetByGenreAsync(genre);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> SearchInShelfAsync(Guid shelfId, string? title, string? author)
        {
            var books = await _bookRepo.SearchInShelfAsync(shelfId, title, author);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}
