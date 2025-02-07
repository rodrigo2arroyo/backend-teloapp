namespace TeloApi.Features.Review.Repositories;
using Models;

public interface IReviewRepository
{
    /// <summary>
    /// Crea una nueva reseña para un hotel.
    /// </summary>
    Task<Review> CreateReviewAsync(Review review);

    /// <summary>
    /// Desactiva (soft delete) una reseña existente.
    /// </summary>
    Task<bool> DeleteReviewAsync(int reviewId);
}