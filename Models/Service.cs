using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<ServicePromotion> ServicePromotions { get; set; } = new List<ServicePromotion>();

    public virtual ICollection<ServiceRate> ServiceRates { get; set; } = new List<ServiceRate>();
}
