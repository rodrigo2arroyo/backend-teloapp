using TeloApi.Features.User.DTOs;

namespace TeloApi.Features.User.Services;

public interface IUserService
{
    Task<bool> RegisterUser(CreateUser request);
    Task<bool> UpdateUser(UpdateUser request);
}