using TeloApi.Contexts;
using TeloApi.Features.Promotion.DTOs;
using TeloApi.Features.Promotion.Repositories;
using TeloApi.Shared;

namespace TeloApi.Features.Promotion.Services;
using Models;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;

    public PromotionService(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<GenericResponse> CreatePromotionAsync(CreatePromotion request)
    {
        var promotion = new Promotion
        {
            HotelId = request.HotelId,
            RateType = request.RateType,
            Description = request.Description,
            Duration = request.Duration,
            PromotionalPrice = request.PromotionalPrice,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            CreatedBy = request.CreatedBy
        };

        var createdPromotion = await _promotionRepository.CreatePromotionAsync(promotion);

        if (request.ServiceIds != null)
        {
            foreach (var serviceId in request.ServiceIds)
            {
                await _promotionRepository.AddServiceToPromotionAsync(createdPromotion.Id, serviceId);
            }
        }

        return new GenericResponse { Id = createdPromotion.Id, Message = "Promotion created successfully." };
    }

    public async Task<GenericResponse> UpdatePromotionAsync(UpdatePromotion request)
    {
        var promotion = await _promotionRepository.UpdatePromotionAsync(new Promotion
        {
            Id = request.Id,
            RateType = request.RateType,
            Description = request.Description,
            Duration = request.Duration ?? 0,
            PromotionalPrice = request.PromotionalPrice ?? 0,
            StartDate = request.StartDate ?? DateTime.UtcNow,
            EndDate = request.EndDate ?? DateTime.UtcNow.AddDays(1),
            Status = request.Status ?? true,
            UpdatedBy = request.UpdatedBy
        });

        await _promotionRepository.RemoveAllServicesFromPromotionAsync(request.Id);

        if (request.ServiceIds != null)
        {
            foreach (var serviceId in request.ServiceIds)
            {
                await _promotionRepository.AddServiceToPromotionAsync(request.Id, serviceId);
            }
        }

        return new GenericResponse { Id = request.Id, Message = "Promotion updated successfully." };
    }
}