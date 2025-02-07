using TeloApi.Shared;

namespace TeloApi.Features.Promotion.Services;
using DTOs;

public interface IPromotionService
{
    Task<GenericResponse> CreatePromotionAsync(CreatePromotion request);
    Task<GenericResponse> UpdatePromotionAsync(UpdatePromotion request);
}