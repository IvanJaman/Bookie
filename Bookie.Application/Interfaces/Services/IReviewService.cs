using Bookie.Application.DTOs;

namespace Bookie.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ReviewDto>> GetByBookIdAsync(Guid bookId);
        Task<ReviewDto> CreateAsync(CreateReviewDto createDto, Guid userId);
        Task<bool> UpdateAsync(Guid reviewId, UpdateReviewDto updateDto, Guid userId);
        Task<bool> DeleteAsync(Guid reviewId, Guid userId);
    }
}
