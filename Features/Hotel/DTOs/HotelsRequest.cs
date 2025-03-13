namespace TeloApi.Features.Hotel.DTOs;

public class HotelsRequest
{
    public List<string>? Names { get; set; } // Filtrar por múltiples nombres
    public List<string>? Cities { get; set; } // Filtrar por múltiples ciudades
    public List<string>? Districts { get; set; } // Filtrar por múltiples distritos
    public decimal? MinPrice { get; set; } // Filtrar por precio mínimo
    public decimal? MaxPrice { get; set; } // Filtrar por precio máximo
    public int PageNumber { get; set; } = 1; // Paginación (página actual)
    public int PageSize { get; set; } = 10; // Tamaño de página
}