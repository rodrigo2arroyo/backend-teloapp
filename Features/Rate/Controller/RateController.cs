using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.Rate.DTOs;
using TeloApi.Features.Rate.Services;

namespace TeloApi.Features.Rate.Controller;

[ApiController]
[Route("api/[controller]")]
public class RateController : ControllerBase
{
    private readonly IRateService _rateService;

    public RateController(IRateService rateService)
    {
        _rateService = rateService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRate([FromBody] CreateRate request)
    {
        var result = await _rateService.CreateRateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRate([FromBody] UpdateRate request)
    {
        var result = await _rateService.UpdateRateAsync(request);
        return Ok(result);
    }
}