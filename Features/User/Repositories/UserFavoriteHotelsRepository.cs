using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;

namespace TeloApi.Features.User.Repositories;
using Models;

public class UserFavoriteHotelsRepository(AppDbContext context) : IUserFavoriteHotelsRepository
{
    public async Task<bool> AddFavorite(Guid userId, int hotelId)
    {
        if (await IsFavorite(userId, hotelId))
            return false; // Ya existe

        var favorite = new UserFavoriteHotel
        {
            UserId = userId,
            HotelId = hotelId,
            CreatedAt = DateTime.UtcNow
        };

        context.UserFavoriteHotels.Add(favorite);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveFavorite(Guid userId, int hotelId)
    {
        var favorite = await context.UserFavoriteHotels
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.HotelId == hotelId);

        if (favorite == null)
            return false;

        context.UserFavoriteHotels.Remove(favorite);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserFavoriteHotel>> GetFavorites(Guid userId)
    {
        return await context.UserFavoriteHotels
            .Where(uf => uf.UserId == userId)
            .Include(uf => uf.Hotel)
            .ToListAsync();
    }

    public async Task<bool> IsFavorite(Guid userId, int hotelId)
    {
        return await context.UserFavoriteHotels
            .AnyAsync(uf => uf.UserId == userId && uf.HotelId == hotelId);
    }
}