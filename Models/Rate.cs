using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Rate
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public int ServiceId { get; set; }

    public string RateType { get; set; } = null!;

    public string? Description { get; set; }

    public int Duration { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
