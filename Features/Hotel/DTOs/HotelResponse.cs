using System.Collections.Generic;
using TeloApi.Features.Hotel.DTOs;

public class HotelResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public LocationDto Location { get; set; }
    public List<RateResponse> Rates { get; set; }
    public List<PromotionResponse> Promotions { get; set; }
    public List<ReviewResponse> Reviews { get; set; }
    public List<string> Images { get; set; } = new List<string>();
}

public class RateResponse
{
    public int Id { get; set; }
    public string RateType { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<ServiceResponse> Services { get; set; }
}

public class PromotionResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal PromotionalPrice { get; set; }
    public List<ServiceResponse> Services { get; set; }
}

public class ReviewResponse
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Rating { get; set; }
}

public class ServiceResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}