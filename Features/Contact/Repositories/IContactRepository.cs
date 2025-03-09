namespace TeloApi.Features.Contact.Repositories;
using Models;

public interface IContactRepository
{
    Task<Contact> CreateContact(Contact contact);
    Task<Contact> UpdateContact(Contact contact);
    Task<bool> DeleteContact(int contactId);
    
}