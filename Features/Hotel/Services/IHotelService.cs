using TeloApi.Features.Hotel.DTOs;
using TeloApi.Shared;

namespace TeloApi.Features.Hotel.Services;

public interface IHotelService
{
    Task<GenericResponse> CreateHotelAsync(CreateHotel request);
    Task<GenericResponse> UpdateHotelAsync(UpdateHotel request);
    Task<HotelResponse> GetHotelByIdAsync(int hotelId);
    Task<GenericResponse> UploadHotelImagesAsync(int hotelId, List<IFormFile> files);
    Task<List<string>> GetHotelImagesAsync(int hotelId);
    
    Task<HotelsResponse> ListHotelsAsync(HotelsRequest request);
}