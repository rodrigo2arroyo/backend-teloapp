using TeloApi.Features.Hotel.Repositories;
using TeloApi.Models;
using TeloApi.Shared;

namespace TeloApi.Features.Hotel.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<GenericResponse> UploadHotelImagesAsync(int hotelId, List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return new GenericResponse { Message = "No files uploaded." };

        if (files.Count > 10)
            return new GenericResponse { Message = "You can upload up to 10 images per hotel." };

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/hotels");
        Directory.CreateDirectory(uploadsFolder); // Asegurar que la carpeta existe

        var uploadedImages = new List<HotelImage>();

        foreach (var file in files)
        {
            var fileName = $"{hotelId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/images/hotels/{fileName}";

            uploadedImages.Add(new HotelImage
            {
                HotelId = hotelId,
                ImageUrl = imageUrl,
                UploadedAt = DateTime.UtcNow
            });
        }

        await _hotelRepository.AddHotelImagesAsync(uploadedImages);

        return new GenericResponse { Message = "Images uploaded successfully." };
    }

    public async Task<List<string>> GetHotelImagesAsync(int hotelId)
    {
        var images = await _hotelRepository.GetHotelImagesAsync(hotelId);
        return images.Select(i => i.ImageUrl).ToList();
    }
}