using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.Hotel.DTOs;
using TeloApi.Features.Hotel.Services;

namespace TeloApi.Features.Hotel.Controller;

[ApiController]
[Route("api/[controller]")]
public class HotelController(IHotelService hotelService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotel request)
    {
        var result = await hotelService.CreateHotelAsync(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateHotel([FromBody] UpdateHotel request)
    {
        var result = await hotelService.UpdateHotelAsync(request);
        return Ok(result);
    }

    [HttpGet("{hotelId}")]
    public async Task<IActionResult> GetHotelById(int hotelId)
    {
        var result = await hotelService.GetHotelByIdAsync(hotelId);
        if (result == null) return NotFound("Hotel not found.");

        return Ok(result);
    }

    [HttpPost("{hotelId}/upload-images")]
    public async Task<IActionResult> UploadHotelImages(int hotelId, [FromForm] List<IFormFile> files)
    {
        var result = await hotelService.UploadHotelImagesAsync(hotelId, files);
        return Ok(result);
    }

    [HttpGet("{hotelId}/images")]
    public async Task<IActionResult> GetHotelImages(int hotelId)
    {
        var images = await hotelService.GetHotelImagesAsync(hotelId);
        if (images == null || images.Count == 0)
            return NotFound("No images found for this hotel.");

        return Ok(images);
    }
}