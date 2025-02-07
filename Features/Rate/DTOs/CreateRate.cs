namespace TeloApi.Features.Rate.DTOs;

public class CreateRate
{
    public int HotelId { get; set; } // ID del hotel relacionado
    public string RateType { get; set; } // Tipo de tarifa
    public string Description { get; set; } // Descripción
    public int Duration { get; set; } // Duración en días
    public decimal Price { get; set; } // Precio
    public bool Status { get; set; } = true; // Estado por defecto activo
    public string CreatedBy { get; set; } // Usuario que crea la tarifa
    public List<int> ServiceIds { get; set; } // Servicios asociados
}

