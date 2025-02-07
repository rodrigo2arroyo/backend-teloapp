using TeloApi.Features.Rate.DTOs;
using TeloApi.Shared;

namespace TeloApi.Features.Rate.Services;

public interface IRateService
{
    Task<GenericResponse> CreateRateAsync(CreateRate request);
    Task<GenericResponse> UpdateRateAsync(UpdateRate request);
}