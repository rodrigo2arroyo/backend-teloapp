namespace TeloApi.Features.Hotel.DTOs;

public class LocationResponse
{
    public string City { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
}