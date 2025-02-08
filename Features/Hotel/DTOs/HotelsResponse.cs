namespace TeloApi.Features.Hotel.DTOs;

public class HotelsResponse
{
    public List<HotelResponse> Hotels { get; set; } // Lista de hoteles
    public int TotalCount { get; set; } // Total de hoteles disponibles
}