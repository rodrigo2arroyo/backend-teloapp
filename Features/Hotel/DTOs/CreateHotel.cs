namespace TeloApi.Features.Hotel.DTOs;

public class CreateHotel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public LocationRequest Location { get; set; } // Datos de ubicación
    public string CreatedBy { get; set; }
}