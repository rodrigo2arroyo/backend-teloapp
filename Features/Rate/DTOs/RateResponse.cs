namespace TeloApi.Features.Rate.DTOs;

public class RateResponse
{
    public int Id { get; set; }
    public string RateType { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public bool Status { get; set; }
    public List<ServiceResponse> Services { get; set; }
}

public class ServiceResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
