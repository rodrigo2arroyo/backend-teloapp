using Microsoft.EntityFrameworkCore;
using TeloApi.Contexts;
using TeloApi.Features.Rate.DTOs;
using TeloApi.Features.Rate.Repositories;


namespace TeloApi.Features.Rate.Services;
using TeloApi.Models;
public class RateService : IRateService
{
    private readonly IRateRepository _rateRepository;
    private readonly AppDbContext _context; // Inyección de DbContext

    public RateService(IRateRepository rateRepository, AppDbContext context)
    {
        _rateRepository = rateRepository;
        _context = context;
    }

    // ✅ CREAR RATE Y ASOCIAR SERVICIOS
    public async Task<RateResponse> CreateRateAsync(CreateRateDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(); // Transacción

        try
        {
            var rate = new Rate
            {
                HotelId = request.HotelId,
                RateType = request.RateType,
                Description = request.Description,
                Duration = request.Duration,
                Price = request.Price,
                Status = request.Status,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rates.Add(rate);
            await _context.SaveChangesAsync(); // Guardar primero para obtener el ID

            if (request.ServiceIds != null && request.ServiceIds.Any())
            {
                var serviceRates = request.ServiceIds.Select(serviceId => new ServiceRate
                {
                    RateId = rate.Id,
                    ServiceId = serviceId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                _context.ServiceRates.AddRange(serviceRates);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return await GetRateByIdAsync(rate.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // ✅ ACTUALIZAR RATE Y MODIFICAR SERVICIOS
    public async Task<RateResponse> UpdateRateAsync(UpdateRateDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(); // Transacción

        try
        {
            var rate = await _context.Rates.FindAsync(request.Id);
            if (rate == null) return null;

            rate.RateType = request.RateType;
            rate.Description = request.Description;
            rate.Duration = request.Duration ?? rate.Duration;
            rate.Price = request.Price ?? rate.Price;
            rate.Status = request.Status ?? rate.Status;
            rate.UpdatedBy = request.UpdatedBy;
            rate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var existingServiceRates = _context.ServiceRates.Where(sr => sr.RateId == rate.Id);
            _context.ServiceRates.RemoveRange(existingServiceRates);
            await _context.SaveChangesAsync();

            if (request.ServiceIds != null && request.ServiceIds.Any())
            {
                var serviceRates = request.ServiceIds.Select(serviceId => new ServiceRate
                {
                    RateId = rate.Id,
                    ServiceId = serviceId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                _context.ServiceRates.AddRange(serviceRates);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return await GetRateByIdAsync(rate.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    public async Task<RateResponse> GetRateByIdAsync(int rateId)
    {
        var rate = await _context.Rates
            .Include(r => r.ServiceRates)
            .ThenInclude(sr => sr.Service) // Cargar los servicios relacionados
            .FirstOrDefaultAsync(r => r.Id == rateId);

        if (rate == null) return null;

        return new RateResponse
        {
            Id = rate.Id,
            RateType = rate.RateType,
            Description = rate.Description,
            Duration = rate.Duration,
            Price = rate.Price,
            Status = rate.Status == true,
            Services = rate.ServiceRates
                .Where(sr => sr.Service != null) // Evitar servicios nulos
                .Select(sr => new ServiceResponse
                {
                    Id = sr.Service.Id,
                    Name = sr.Service.Name
                }).ToList()
        };
    }
    
    public async Task<bool> DeleteRateAsync(DeleteRateDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(); // Transacción

        try
        {
            var rate = await _context.Rates.FindAsync(request.Id);
            if (rate == null) return false;

            _context.Rates.Remove(rate);

            var relatedServiceRates = _context.ServiceRates.Where(sr => sr.RateId == rate.Id);
            _context.ServiceRates.RemoveRange(relatedServiceRates);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}