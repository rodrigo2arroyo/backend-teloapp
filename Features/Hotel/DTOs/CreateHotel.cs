namespace TeloApi.Features.Hotel.DTOs;

public class CreateHotel
{
    public string Name { get; set; }
    public LocationDto Location { get; set; } // Datos de ubicación
    public string CreatedBy { get; set; }
}