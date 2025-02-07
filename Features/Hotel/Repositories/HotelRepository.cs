using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;

namespace TeloApi.Features.Hotel.Repositories;
using Models;

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
    
    public async Task<Hotel> CreateHotelAsync(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> UpdateHotelAsync(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> GetHotelByIdWithDetailsAsync(int hotelId)
    {
        return await _context.Hotels
            .Include(h => h.Location)
            .Include(h => h.Rates)
            .ThenInclude(r => r.ServiceRates)
            .ThenInclude(sr => sr.Service)
            .Include(h => h.Promotions)
            .ThenInclude(p => p.ServicePromotions)
            .ThenInclude(sp => sp.Service)
            .Include(h => h.Reviews)
            .FirstOrDefaultAsync(h => h.Id == hotelId);
    }
}