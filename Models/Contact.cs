using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class Contact
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? CountryCode { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
