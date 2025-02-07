namespace TeloApi.Features.Review.DTOs;

public class CreateReview
{
    public int HotelId { get; set; }
    public string Description { get; set; }
    public decimal Rating { get; set; } // Decimal (4,2)
    public string Author { get; set; } // Antes "UserName"
    public string CreatedBy { get; set; }
}
