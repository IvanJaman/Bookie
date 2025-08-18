using Bookie.Application.DTOs;
using Bookie.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelvesController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        // api/Shelves/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var shelf = await _shelfService.GetByIdAsync(id);
            if (shelf == null)
                return NotFound();

            return Ok(shelf);
        }

        // api/Shelves/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var shelves = await _shelfService.GetByUserIdAsync(userId);
            return Ok(shelves);
        }

        // api/Shelves
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShelfDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Guid.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new Exception("User ID not found in token."));

            var shelf = await _shelfService.CreateShelfAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = shelf.Id }, shelf);
        }

        // api/Shelves/Rename/{id}
        [HttpPut("Rename/{id}")]
        public async Task<IActionResult> Rename(Guid id, [FromBody] RenameShelfDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NewName))
                return BadRequest("New shelf name cannot be empty.");

            var renamed = await _shelfService.RenameShelfAsync(id, dto.NewName);
            if (!renamed)
                return NotFound();

            return NoContent();
        }

        // api/Shelves/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _shelfService.DeleteShelfAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // api/Shelves/{shelfId}/books/{bookId}
        [Authorize]
        [HttpPost("{shelfId}/books/{bookId}")]
        public async Task<IActionResult> AddBook(Guid shelfId, Guid bookId)
        {
            var added = await _shelfService.AddBookToShelfAsync(shelfId, bookId);
            if (!added)
                return NotFound();

            return NoContent();
        }

        // api/Shelves/{shelfId}/books/{bookId}
        [Authorize]
        [HttpDelete("{shelfId}/books/{bookId}")]
        public async Task<IActionResult> RemoveBook(Guid shelfId, Guid bookId)
        {
            var removed = await _shelfService.RemoveBookFromShelfAsync(shelfId, bookId);
            if (!removed)
                return NotFound();

            return NoContent();
        }
    }
}
