using TeloApi.Features.Rate.DTOs;
using TeloApi.Features.Rate.Repositories;
using TeloApi.Shared;

namespace TeloApi.Features.Rate.Services;
using Models;
public class RateService(IRateRepository rateRepository) : IRateService
{
    public async Task<GenericResponse> CreateRateAsync(CreateRate request)
    {
        var rate = new Rate
        {
            HotelId = request.HotelId,
            RateType = request.RateType,
            Description = request.Description,
            Duration = request.Duration,
            Price = request.Price,
            Status = request.Status,
            CreatedBy = request.CreatedBy
        };

        var createdRate = await rateRepository.CreateRateAsync(rate);

        if (request.ServiceIds != null)
        {
            foreach (var serviceId in request.ServiceIds)
            {
                await rateRepository.AddServiceToRateAsync(createdRate.Id, serviceId);
            }
        }

        return new GenericResponse { Id = createdRate.Id, Message = "Rate created successfully." };
    }

    public async Task<GenericResponse> UpdateRateAsync(UpdateRate request)
    {
        var rate = await rateRepository.UpdateRateAsync(new Rate
        {
            Id = request.Id,
            RateType = request.RateType,
            Description = request.Description,
            Duration = request.Duration ?? 0,
            Price = request.Price ?? 0,
            Status = request.Status ?? true,
            UpdatedBy = request.UpdatedBy,
        });

        await rateRepository.RemoveAllServicesFromRateAsync(request.Id);

        if (request.ServiceIds != null)
        {
            foreach (var serviceId in request.ServiceIds)
            {
                await rateRepository.AddServiceToRateAsync(request.Id, serviceId);
            }
        }

        return new GenericResponse { Id = request.Id, Message = "Rate updated successfully." };
    }
}