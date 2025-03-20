using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class UserFavoriteHotel
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int HotelId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
