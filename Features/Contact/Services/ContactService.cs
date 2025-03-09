using TeloApi.Features.Contact.DTOs;
using TeloApi.Features.Contact.Repositories;
using TeloApi.Shared;

namespace TeloApi.Features.Contact.Services;
using Models;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    public async Task<GenericResponse> CreateContact(CreateContact request)
    {
        var contact = new Contact
        {
            HotelId = request.HotelId,
            Firstname = request.FirstName,
            Lastname = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            CountryCode = request.CountryCode,
            Status = request.Status,
            CreatedBy = request.CreatedBy
        };
        
        var createdContact = await contactRepository.CreateContact(contact);
        
        return new GenericResponse { Id = createdContact.Id, Message = "Contact created successfully." };
    }

    public async Task<GenericResponse> UpdateContact(UpdateContact request)
    {
        var contact = new Contact
        {
            Id = request.Id,
            Firstname = request.FirstName,
            Lastname = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            CountryCode = request.CountryCode,
            UpdatedBy = request.UpdatedBy,
            UpdatedAt = DateTime.UtcNow
        };
        
        var updatedContact = await contactRepository.UpdateContact(contact);
        
        return new GenericResponse { Id = updatedContact.Id, Message = "Contact updated successfully." };
    }

    public async Task<GenericResponse> DeleteContact(int contactId)
    {
        var isDeleted = await contactRepository.DeleteContact(contactId);
        if (!isDeleted)
            return new GenericResponse { Message = "Contact not found or already deleted." };

        return new GenericResponse { Id = contactId, Message = "Contact deleted successfully." };
    }
}