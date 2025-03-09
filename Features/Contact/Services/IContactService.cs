using TeloApi.Features.Contact.DTOs;
using TeloApi.Shared;

namespace TeloApi.Features.Contact.Services;

public interface IContactService
{
    Task<GenericResponse> CreateContact(CreateContact request);
    Task<GenericResponse> UpdateContact(UpdateContact request);
    Task<GenericResponse> DeleteContact(int contactId);
}