using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;
using TeloApi.Models;

namespace TeloApi.Features.Hotel.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddHotelImagesAsync(List<HotelImage> images)
    {
        await _context.HotelImages.AddRangeAsync(images);
        await _context.SaveChangesAsync();
    }

    public async Task<List<HotelImage>> GetHotelImagesAsync(int hotelId)
    {
        return await _context.HotelImages.Where(i => i.HotelId == hotelId).ToListAsync();
    }
}