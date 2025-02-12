using Swashbuckle.AspNetCore.Annotations;

namespace TeloApi.Features.Hotel.DTOs;

public class HotelsResult
{
    [SwaggerSchema("Lista de hoteles disponibles")]
    public List<HotelResponse> Hotels { get; set; }

    [SwaggerSchema("Número total de hoteles")]
    public int TotalCount { get; set; }
}