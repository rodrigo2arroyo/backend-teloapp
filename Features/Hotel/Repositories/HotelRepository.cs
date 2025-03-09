using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;
using TeloApi.Features.Hotel.DTOs;

namespace TeloApi.Features.Hotel.Repositories;
using Models;

public class HotelRepository(AppDbContext context) : IHotelRepository
{
    public async Task AddHotelImagesAsync(List<HotelImage> images)
    {
        await context.HotelImages.AddRangeAsync(images);
        await context.SaveChangesAsync();
    }

    public async Task<List<HotelImage>> GetHotelImagesAsync(int hotelId)
    {
        return await context.HotelImages.Where(i => i.HotelId == hotelId).ToListAsync();
    }
    
    public async Task<Hotel> CreateHotelAsync(Hotel hotel)
    {
        await context.Hotels.AddAsync(hotel);
        await context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> UpdateHotelAsync(Hotel hotel)
    {
        context.Hotels.Update(hotel);
        await context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> GetHotelByIdWithDetailsAsync(int hotelId)
    {
        return await context.Hotels
            .Include(h => h.Location)
            .Include(h => h.Rates)
            .ThenInclude(r => r.ServiceRates)
            .ThenInclude(sr => sr.Service)
            .Include(h => h.Promotions)
            .ThenInclude(p => p.ServicePromotions)
            .ThenInclude(sp => sp.Service)
            .Include(h => h.Reviews)
            .Include(h => h.HotelImages)
            .FirstOrDefaultAsync(h => h.Id == hotelId);
    }
    
    public async Task<(List<Hotel>, int)> GetHotelsAsync(HotelsRequest filters)
    {
        var query = context.Hotels
            .Include(h => h.Location)
            .Include(h => h.Rates)
            .ThenInclude(r => r.ServiceRates)
            .ThenInclude(sr => sr.Service)
            .Include(h => h.Promotions)
            .ThenInclude(p => p.ServicePromotions)
            .ThenInclude(sp => sp.Service)
            .Include(h => h.Reviews)
            .Include(h => h.HotelImages)
            .AsQueryable();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(h => EF.Functions.Like(h.Name, $"%{filters.Name}%"));

        if (!string.IsNullOrEmpty(filters.City))
            query = query.Where(h => h.Location.City == filters.City);

        if (!string.IsNullOrEmpty(filters.District))
            query = query.Where(h => h.Location.District == filters.District);

        if (filters.MinPrice.HasValue)
            query = query.Where(h => h.Rates.Any(r => r.Price >= filters.MinPrice.Value));

        if (filters.MaxPrice.HasValue)
            query = query.Where(h => h.Rates.Any(r => r.Price <= filters.MaxPrice.Value));

        // Contar el total de resultados antes de aplicar la paginación
        var totalCount = await query.CountAsync();

        // Aplicar paginación obligatoria
        query = query
            .Skip((filters.PageNumber - 1) * filters.PageSize)
            .Take(filters.PageSize);

        // Ejecutar la consulta
        var hotels = await query.ToListAsync();

        return (hotels, totalCount);
    }
}