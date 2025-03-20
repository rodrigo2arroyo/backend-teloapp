namespace TeloApi.Features.User.Repositories;
using Models;

public interface IUserFavoriteHotelsRepository
{
    Task<bool> AddFavorite(Guid userId, int hotelId);
    Task<bool> RemoveFavorite(Guid userId, int hotelId);
    Task<List<UserFavoriteHotel>> GetFavorites(Guid userId);
    Task<bool> IsFavorite(Guid userId, int hotelId);
}