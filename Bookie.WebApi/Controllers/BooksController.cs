﻿using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // api/Books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        // api/Books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // api/Books/search?title=...&author=...
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author)
        {
            var results = await _bookService.SearchAsync(title, author);
            return Ok(results);
        }

        // api/Books
        [HttpPost]
        [Authorize(Roles = "Publisher,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto newBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Guid.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new Exception("User ID not found in token."));

            var createdBook = await _bookService.CreateAsync(newBook, userId);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        // api/Books/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Publisher,Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookDto updatedBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _bookService.UpdateAsync(id, updatedBook);
            if (!success) return NotFound();

            return NoContent();
        }

        // api/Books/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Publisher,Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _bookService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
