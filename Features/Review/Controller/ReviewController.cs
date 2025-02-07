using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.Review.DTOs;
using TeloApi.Features.Review.Services;

namespace TeloApi.Features.Review.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Crear una nueva reseña para un hotel.
    /// </summary>
    /// <param name="request">Datos de la nueva reseña.</param>
    /// <returns>Resultado de la creación.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReview request)
    {
        var result = await _reviewService.CreateReviewAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Eliminar (soft delete) una reseña.
    /// </summary>
    /// <param name="reviewId">ID de la reseña a eliminar.</param>
    /// <param name="deletedBy">Quién está realizando la eliminación.</param>
    /// <returns>Resultado de la eliminación.</returns>
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId, [FromQuery] string deletedBy)
    {
        var result = await _reviewService.DeleteReviewAsync(reviewId, deletedBy);
        return Ok(result);
    }
}