namespace TeloApi.Features.Rate.DTOs;

public class UpdateRateDto
{
    public int Id { get; set; } // ID de la tarifa que se actualizará
    public string RateType { get; set; } // Tipo de tarifa (opcional)
    public string Description { get; set; } // Descripción (opcional)
    public int? Duration { get; set; } // Duración en días (opcional)
    public decimal? Price { get; set; } // Precio (opcional)
    public bool? Status { get; set; } // Estado (opcional)
    public string UpdatedBy { get; set; } // Usuario que actualiza la tarifa
    public List<int> ServiceIds { get; set; } // Nuevos servicios asociados (opcional)
}