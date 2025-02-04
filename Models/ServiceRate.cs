using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class ServiceRate
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int RateId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Rate Rate { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
