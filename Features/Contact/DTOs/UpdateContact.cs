namespace TeloApi.Features.Contact.DTOs;

public class UpdateContact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; }
    public string? CountryCode { get; set; }
    public string UpdatedBy { get; set; }
}