namespace TeloApi.Features.Hotel.DTOs;

public class UpdateHotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public LocationDto Location { get; set; } // Datos de ubicación
    public string UpdatedBy { get; set; }
}