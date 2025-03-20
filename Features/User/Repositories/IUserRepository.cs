namespace TeloApi.Features.User.Repositories;
using Models;

public interface IUserRepository
{
    Task<bool> EmailExists(string email);
    Task CreateUser(User user);
    Task<User?> GetUserById(Guid id);
    Task UpdateUser(User user);
}