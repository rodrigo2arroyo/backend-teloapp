namespace TeloApi.Features.Review.DTOs;

public class DeleteReview
{
    public int Id { get; set; }
    public string DeletedBy { get; set; } // Indica quién eliminó la reseña
}