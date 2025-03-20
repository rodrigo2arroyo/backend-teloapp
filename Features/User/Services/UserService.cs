using TeloApi.Features.User.DTOs;
using TeloApi.Features.User.Repositories;
using BCrypt.Net;
namespace TeloApi.Features.User.Services;
using Models;

public class UserService(IUserRepository userRepository, IEmailVerificationRepository emailVerificationRepository)
    : IUserService
{
    public async Task<bool> RegisterUser(CreateUser request)
    {
        if (await userRepository.EmailExists(request.Email))
            throw new Exception("El email ya está registrado.");

        if (!await emailVerificationRepository.IsEmailVerified(request.Email))
            throw new Exception("Debe verificar su email antes de registrarse.");

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.CreateUser(newUser);
        return true;
    }

    public async Task<bool> UpdateUser(UpdateUser request)
    {
        var user = await userRepository.GetUserById(request.Id);
        if (user == null)
            throw new Exception("Usuario no encontrado.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateUser(user);
        return true;
    }
}