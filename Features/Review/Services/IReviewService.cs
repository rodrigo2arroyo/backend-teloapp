using TeloApi.Features.Review.DTOs;
using TeloApi.Shared;

namespace TeloApi.Features.Review.Services;

public interface IReviewService
{
    /// <summary>
    /// Crea una nueva reseña y la vincula con el hotel.
    /// </summary>
    Task<GenericResponse> CreateReviewAsync(CreateReview request);

    /// <summary>
    /// Elimina (soft delete) una reseña existente por su ID.
    /// </summary>
    Task<GenericResponse> DeleteReviewAsync(int reviewId);
}