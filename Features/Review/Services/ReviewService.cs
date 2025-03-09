using TeloApi.Features.Review.DTOs;
using TeloApi.Features.Review.Repositories;
using TeloApi.Shared;

namespace TeloApi.Features.Review.Services;
using Models;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<GenericResponse> CreateReviewAsync(CreateReview request)
    {
        var review = new Review
        {
            HotelId = request.HotelId,
            Description = request.Description,
            Rating = request.Rating,
            Author = request.Author,
            CreatedBy = request.CreatedBy,
            CreatedAt = DateTime.UtcNow,
            Status = true
        };

        var createdReview = await _reviewRepository.CreateReviewAsync(review);

        return new GenericResponse { Id = createdReview.Id, Message = "Review created successfully." };
    }

    public async Task<GenericResponse> DeleteReviewAsync(int reviewId)
    {
        var isDeleted = await _reviewRepository.DeleteReviewAsync(reviewId);
        if (!isDeleted)
            return new GenericResponse { Message = "Review not found or already deleted." };

        return new GenericResponse { Id = reviewId, Message = "Review deleted successfully." };
    }
}