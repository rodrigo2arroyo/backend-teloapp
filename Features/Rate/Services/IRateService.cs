namespace TeloApi.Features.Rate.Services;
using TeloApi.Features.Rate.DTOs;

public interface IRateService
{
    Task<RateResponse> CreateRateAsync(CreateRateDto request);
    Task<RateResponse> UpdateRateAsync(UpdateRateDto request);
    Task<RateResponse> GetRateByIdAsync(int rateId);
    Task<bool> DeleteRateAsync(DeleteRateDto request);
}