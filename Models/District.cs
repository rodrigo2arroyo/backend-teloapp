using System;
using System.Collections.Generic;

namespace TeloApi.Models;

public partial class District
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;
}
