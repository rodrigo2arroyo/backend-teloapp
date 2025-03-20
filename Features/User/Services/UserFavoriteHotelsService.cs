using TeloApi.Features.User.DTOs;
using TeloApi.Features.User.Repositories;

namespace TeloApi.Features.User.Services;

public class UserFavoriteHotelsService(IUserFavoriteHotelsRepository favoriteRepository) : IUserFavoriteHotelsService
{
    public async Task<bool> AddFavorite(AddFavorite request)
    {
        if (!Guid.TryParse(request.UserId.ToLower(), out Guid userId))
            throw new Exception("El UserId no es un GUID válido.");

        return await favoriteRepository.AddFavorite(userId, request.HotelId);
    }

    public async Task<bool> RemoveFavorite(RemoveFavorite request)
    {
        if (!Guid.TryParse(request.UserId.ToLower(), out Guid userId))
            throw new Exception("El UserId no es un GUID válido.");
        
        return await favoriteRepository.RemoveFavorite(userId, request.HotelId);
    }

    public async Task<List<FavoriteHotelsResponse>> GetFavorites(Guid userId)
    {
        var favorites = await favoriteRepository.GetFavorites(userId);
        return favorites.Select(f => new FavoriteHotelsResponse
        {
            HotelId = f.HotelId,
            HotelName = f.Hotel.Name // Suponiendo que Hotel tiene un campo Name
        }).ToList();
    }
}