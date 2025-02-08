using TeloApi.Features.Hotel.DTOs;

namespace TeloApi.Features.Hotel.Repositories;
using Models;

public interface IHotelRepository
{
    Task<Hotel> CreateHotelAsync(Hotel hotel);
    Task<Hotel> UpdateHotelAsync(Hotel hotel);
    Task<Hotel> GetHotelByIdWithDetailsAsync(int hotelId);
    Task AddHotelImagesAsync(List<HotelImage> images);
    Task<List<HotelImage>> GetHotelImagesAsync(int hotelId);
    
    //List Hotels
    Task<(List<Hotel>, int)> GetHotelsAsync(HotelsRequest filters);
}