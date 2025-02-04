namespace TeloApi.Features.Rate.Repositories;
using TeloApi.Models;

public interface IRateRepository
{
    Task<Rate> CreateRateAsync(Rate rate, List<int> serviceIds);
    Task<Rate> UpdateRateAsync(Rate rate, List<int> serviceIds);
    Task<Rate> GetRateByIdAsync(int rateId);
    Task<bool> DeleteRateAsync(int rateId);
}