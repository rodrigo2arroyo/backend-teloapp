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

    // ✅ CREAR RATE
    [HttpPost]
    public async Task<IActionResult> CreateRate([FromBody] CreateRateDto request)
    {
        var result = await _rateService.CreateRateAsync(request);
        if (result == null) return BadRequest("Failed to create rate.");
        return Ok(result);
    }

    // ✅ ACTUALIZAR RATE
    [HttpPut]
    public async Task<IActionResult> UpdateRate([FromBody] UpdateRateDto request)
    {
        var result = await _rateService.UpdateRateAsync(request);
        if (result == null) return NotFound("Rate not found.");
        return Ok(result);
    }

    // ✅ OBTENER RATE POR ID
    [HttpGet("{rateId}")]
    public async Task<IActionResult> GetRateById(int rateId)
    {
        var result = await _rateService.GetRateByIdAsync(rateId);
        if (result == null) return NotFound("Rate not found.");
        return Ok(result);
    }

    // ✅ ELIMINAR RATE
    [HttpDelete]
    public async Task<IActionResult> DeleteRate([FromBody] DeleteRateDto request)
    {
        var success = await _rateService.DeleteRateAsync(request);
        if (!success) return NotFound("Rate not found.");
        return NoContent();
    }
}