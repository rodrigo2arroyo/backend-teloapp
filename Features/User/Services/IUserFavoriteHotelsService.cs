using TeloApi.Features.User.DTOs;

namespace TeloApi.Features.User.Services;

public interface IUserFavoriteHotelsService
{
    Task<bool> AddFavorite(AddFavorite request);
    Task<bool> RemoveFavorite(RemoveFavorite request);
    Task<List<FavoriteHotelsResponse>> GetFavorites(Guid userId);
}