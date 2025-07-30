using AutoMapper;
using Bookie.Application.DTOs;
using Bookie.Application.Interfaces;
using Bookie.Application.Interfaces.Services;
using Bookie.Domain.Entities;

namespace Bookie.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepo, IBookRepository bookRepo, IUserRepository userRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
                throw new Exception("Review not found.");

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetByBookIdAsync(Guid bookId)
        {
            var reviews = await _reviewRepo.GetByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto createDto, Guid userId)
        {
            var book = await _bookRepo.GetByIdAsync(createDto.BookId);
            if (book == null)
                throw new Exception("Book not found.");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            var review = new Review
            {
                Id = Guid.NewGuid(),
                BookId = createDto.BookId,
                UserId = userId,
                Rating = createDto.Rating,
                Text = createDto.Text,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepo.AddAsync(review);

            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<bool> UpdateAsync(Guid reviewId, UpdateReviewDto updateDto, Guid userId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null)
                return false;

            if (review.UserId != userId)
                throw new UnauthorizedAccessException("You cannot edit someone else's review.");

            review.Rating = updateDto.Rating;
            review.Text = updateDto.Text;

            await _reviewRepo.UpdateAsync(review);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid reviewId, Guid userId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if (review == null)
                return false;

            var requestingUser = await _userRepo.GetByIdAsync(userId);
            if (requestingUser == null)
                throw new Exception("User not found.");

            bool isOwner = review.UserId == userId;
            bool isAdmin = requestingUser.Role?.Name == "Admin";

            if (!isOwner && !isAdmin)
                throw new UnauthorizedAccessException("You cannot delete this review.");

            await _reviewRepo.DeleteAsync(reviewId);
            return true;
        }
    }
}
