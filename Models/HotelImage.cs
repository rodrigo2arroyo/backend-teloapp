using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class HotelImage
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
