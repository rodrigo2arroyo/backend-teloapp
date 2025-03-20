using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class EmailVerification
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string VerificationCode { get; set; } = null!;

    public DateTime ExpirationTime { get; set; }

    public bool? Verified { get; set; }

    public DateTime? CreatedAt { get; set; }
}
