using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;

namespace TeloApi.Features.User.Repositories;
using Models;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task CreateUser(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task UpdateUser(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}