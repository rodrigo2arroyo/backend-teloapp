using TeloApi.Shared;

namespace TeloApi.Features.Hotel.Services;

public interface IHotelService
{
    Task<GenericResponse> UploadHotelImagesAsync(int hotelId, List<IFormFile> files);
    Task<List<string>> GetHotelImagesAsync(int hotelId);
}