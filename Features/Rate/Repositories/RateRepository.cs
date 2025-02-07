using TeloApi.Contexts;

namespace TeloApi.Features.Rate.Repositories;
using Models;

public class RateRepository : IRateRepository
{
    private readonly AppDbContext _context;

    public RateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Rate> CreateRateAsync(Rate rate)
    {
        await _context.Rates.AddAsync(rate);
        await _context.SaveChangesAsync();
        return rate;
    }

    public async Task<Rate> UpdateRateAsync(Rate rate)
    {
        _context.Rates.Update(rate);
        await _context.SaveChangesAsync();
        return rate;
    }

    public async Task AddServiceToRateAsync(int rateId, int serviceId)
    {
        var serviceRate = new ServiceRate
        {
            RateId = rateId,
            ServiceId = serviceId
        };

        await _context.ServiceRates.AddAsync(serviceRate);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAllServicesFromRateAsync(int rateId)
    {
        var services = _context.ServiceRates.Where(sp => sp.RateId == rateId);
        _context.ServiceRates.RemoveRange(services);
        await _context.SaveChangesAsync();
    }
}