namespace TeloApi.Features.Promotion.DTOs;

public class CreatePromotion
{
    public int HotelId { get; set; }
    public string RateType { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public decimal PromotionalPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Status { get; set; } = true;
    public string CreatedBy { get; set; }
    public List<int> ServiceIds { get; set; } // Lista de servicios asociados
}