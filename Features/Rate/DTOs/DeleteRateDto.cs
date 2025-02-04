namespace TeloApi.Features.Rate.DTOs;

public class DeleteRateDto
{
    public int Id { get; set; } // ID de la tarifa a eliminar
    public string DeletedBy { get; set; } // Usuario que solicita la eliminación
}