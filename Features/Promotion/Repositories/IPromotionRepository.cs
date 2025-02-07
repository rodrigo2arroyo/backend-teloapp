
namespace TeloApi.Features.Promotion.Repositories;
using Models;

public interface IPromotionRepository
{
    Task<Promotion> CreatePromotionAsync(Promotion promotion);
    Task<Promotion> UpdatePromotionAsync(Promotion promotion);
    Task AddServiceToPromotionAsync(int promotionId, int serviceId);
    Task RemoveAllServicesFromPromotionAsync(int promotionId);
}