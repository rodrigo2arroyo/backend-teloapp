namespace TeloApi.Features.Promotion.DTOs;

public class UpdatePromotion
{
    public int Id { get; set; }
    public string RateType { get; set; }
    public string Description { get; set; }
    public int? Duration { get; set; }
    public decimal? PromotionalPrice { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? Status { get; set; }
    public string UpdatedBy { get; set; }
    public List<int> ServiceIds { get; set; } // Lista de servicios asociados
}