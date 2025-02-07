using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? LocationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
