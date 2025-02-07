using TeloApi.Contexts;

namespace TeloApi.Features.Promotion.Repositories;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

public class PromotionRepository(AppDbContext context) : IPromotionRepository
{
    public async Task<Promotion> CreatePromotionAsync(Promotion promotion)
    {
        await context.Promotions.AddAsync(promotion);
        await context.SaveChangesAsync();
        return promotion;
    }

    public async Task<Promotion> UpdatePromotionAsync(Promotion promotion)
    {
        context.Promotions.Update(promotion);
        await context.SaveChangesAsync();
        return promotion;
    }

    public async Task AddServiceToPromotionAsync(int promotionId, int serviceId)
    {
        var servicePromotion = new ServicePromotion
        {
            PromotionId = promotionId,
            ServiceId = serviceId
        };

        await context.ServicePromotions.AddAsync(servicePromotion);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAllServicesFromPromotionAsync(int promotionId)
    {
        var services = context.ServicePromotions.Where(sp => sp.PromotionId == promotionId);
        context.ServicePromotions.RemoveRange(services);
        await context.SaveChangesAsync();
    }
}