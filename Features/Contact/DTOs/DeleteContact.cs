namespace TeloApi.Features.Contact.DTOs;

public class DeleteContact
{
    public int contactId { get; set; }
    public string DeletedBy { get; set; }
}