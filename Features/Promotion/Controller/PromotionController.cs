using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.Promotion.DTOs;
using TeloApi.Features.Promotion.Services;

namespace TeloApi.Features.Promotion.Controller;

[ApiController]
[Route("api/[controller]")]
public class PromotionController : ControllerBase
{
    private readonly IPromotionService _promotionService;

    public PromotionController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotion request)
    {
        var result = await _promotionService.CreatePromotionAsync(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePromotion([FromBody] UpdatePromotion request)
    {
        var result = await _promotionService.UpdatePromotionAsync(request);
        return Ok(result);
    }
}