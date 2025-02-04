using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;
using TeloApi.Models;
// Replace with your actual namespace

// Replace with your actual namespace

namespace TeloApi.Features.Rate.Repositories;

public class RateRepository(AppDbContext context) : IRateRepository
{
    public async Task<Models.Rate> CreateRateAsync(Models.Rate rate, List<int> serviceIds)
    {
        await context.Rates.AddAsync(rate);
        await context.SaveChangesAsync();

        foreach (var serviceId in serviceIds)
        {
            context.ServiceRates.Add(new ServiceRate
            {
                RateId = rate.Id,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.SaveChangesAsync();
        return rate;
    }

    public async Task<Models.Rate> UpdateRateAsync(Models.Rate rate, List<int> serviceIds)
    {
        var existingRate = await context.Rates.FindAsync(rate.Id);
        if (existingRate == null) return null;

        existingRate.RateType = rate.RateType;
        existingRate.Description = rate.Description;
        existingRate.Duration = rate.Duration;
        existingRate.Price = rate.Price;
        existingRate.Status = rate.Status;
        existingRate.UpdatedBy = rate.UpdatedBy;
        existingRate.UpdatedAt = rate.UpdatedAt;

        // Remove old service associations
        var existingServiceRates = context.ServiceRates.Where(sr => sr.RateId == rate.Id);
        context.ServiceRates.RemoveRange(existingServiceRates);

        // Add new service associations
        foreach (var serviceId in serviceIds)
        {
            context.ServiceRates.Add(new ServiceRate
            {
                RateId = rate.Id,
                ServiceId = serviceId,
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.SaveChangesAsync();
        return existingRate;
    }

    public async Task<Models.Rate> GetRateByIdAsync(int rateId)
    {
        return await context.Rates
            .Include(r => r.ServiceRates)
            .ThenInclude(sr => sr.Service)
            .FirstOrDefaultAsync(r => r.Id == rateId);
    }

    public async Task<bool> DeleteRateAsync(int rateId)
    {
        var rate = await context.Rates.FindAsync(rateId);
        if (rate == null) return false;

        context.Rates.Remove(rate);

        var relatedServiceRates = context.ServiceRates.Where(sr => sr.RateId == rateId);
        context.ServiceRates.RemoveRange(relatedServiceRates);

        await context.SaveChangesAsync();
        return true;
    }
}