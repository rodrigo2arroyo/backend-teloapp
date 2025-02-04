using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class ServicePromotion
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int PromotionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Promotion Promotion { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
