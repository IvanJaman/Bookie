using Bookie.Application.DTOs;
using Bookie.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // api/Reviews/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // api/Reviews/book/{bookId}
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(Guid bookId)
        {
            var reviews = await _reviewService.GetByBookIdAsync(bookId);
            return Ok(reviews);
        }

        // api/Reviews
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Guid.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new Exception("User ID not found in token."));

            var review = await _reviewService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
        }

        // api/Reviews/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = Guid.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new Exception("User ID not found in token."));

            var updated = await _reviewService.UpdateAsync(id, dto, userId);
            if (!updated)
                return Forbid(); 

            return NoContent();
        }

        // api/Reviews/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? throw new Exception("User ID not found in token."));

            var deleted = await _reviewService.DeleteAsync(id, userId);
            if (!deleted)
                return Forbid(); 

            return NoContent();
        }
    }
}
