using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.Hotel.Services;

namespace TeloApi.Features.Hotel.Controller;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpPost("{hotelId}/upload-images")]
    public async Task<IActionResult> UploadHotelImages(int hotelId, [FromForm] List<IFormFile> files)
    {
        var result = await _hotelService.UploadHotelImagesAsync(hotelId, files);
        return Ok(result);
    }

    [HttpGet("{hotelId}/images")]
    public async Task<IActionResult> GetHotelImages(int hotelId)
    {
        var images = await _hotelService.GetHotelImagesAsync(hotelId);
        if (images == null || images.Count == 0)
            return NotFound("No images found for this hotel.");

        return Ok(images);
    }
}