using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Location
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Street { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
