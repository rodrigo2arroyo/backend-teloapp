using TeloApi.Contexts;

namespace TeloApi.Features.Contact.Repositories;
using Models;

public class ContactRepository(AppDbContext context) : IContactRepository
{
    public async Task<Contact> CreateContact(Contact contact)
    {
        await context.Contacts.AddAsync(contact);
        await context.SaveChangesAsync();
        return contact;
    }
    
    public async Task<Contact> UpdateContact(Contact contact)
    {
        context.Contacts.Update(contact);
        await context.SaveChangesAsync();
        return contact;
    }
    
    public async Task<bool> DeleteContact(int contactId)
    {
        var contact = await context.Contacts.FindAsync(contactId);
        if (contact == null) return false;

        // Soft delete: marcar la reseña como inactiva
        contact.Status = false;
        contact.UpdatedAt = DateTime.UtcNow;
        
        await context.SaveChangesAsync();
        return true;
    }
}