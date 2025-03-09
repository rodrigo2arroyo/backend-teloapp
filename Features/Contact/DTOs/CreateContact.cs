namespace TeloApi.Features.Contact.DTOs;

public class CreateContact
{
    public int HotelId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CountryCode { get; set; }
    public bool Status { get; set; }
    public string CreatedBy { get; set; }
}