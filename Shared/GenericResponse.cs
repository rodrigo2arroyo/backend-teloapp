namespace TeloApi.Shared;

public class GenericResponse
{
    public int? Id { get; set; } // Puede ser null en caso de error
    public string Message { get; set; }
}