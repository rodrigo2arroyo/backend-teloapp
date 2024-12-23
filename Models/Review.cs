using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Review
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public string Description { get; set; } = null!;

    public decimal? Rating { get; set; }

    public string? Author { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
