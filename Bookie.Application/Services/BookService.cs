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
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
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
            var entity = _mapper.Map<Book>(newBook);
            entity.CreatedByUserId = createdByUserId;
            await _bookRepo.AddAsync(entity);
            return _mapper.Map<BookDto>(entity);
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
    }
}
