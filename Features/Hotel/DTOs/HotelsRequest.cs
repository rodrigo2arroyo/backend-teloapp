namespace TeloApi.Features.Hotel.DTOs;

public class HotelsRequest
{
    public string? Name { get; set; } // Filtrar por nombre del hotel
    public string? City { get; set; } // Filtrar por ciudad
    public string? District { get; set; } // Filtrar por distrito
    public decimal? MinPrice { get; set; } // Filtrar por precio mínimo (Rates)
    public decimal? MaxPrice { get; set; } // Filtrar por precio máximo (Rates)
    public int PageNumber { get; set; } = 1; // Paginación (página actual)
    public int PageSize { get; set; } = 10; // Tamaño de página
}