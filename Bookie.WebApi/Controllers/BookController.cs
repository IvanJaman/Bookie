using Bookie.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.WebApi.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
    }
}
