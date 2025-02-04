using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Promotion
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public string RateType { get; set; } = null!;

    public string? Description { get; set; }

    public int Duration { get; set; }

    public decimal PromotionalPrice { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<ServicePromotion> ServicePromotions { get; set; } = new List<ServicePromotion>();
}
