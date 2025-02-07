namespace TeloApi.Features.Rate.Repositories;
using Models;

public interface IRateRepository
{
    Task<Rate> CreateRateAsync(Rate rate);
    Task<Rate> UpdateRateAsync(Rate rate);
    Task AddServiceToRateAsync(int rateId, int serviceId);
    Task RemoveAllServicesFromRateAsync(int rateId);
}