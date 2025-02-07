namespace TeloApi.Features.Hotel.Repositories;
using Models;

public interface IHotelRepository
{
    Task AddHotelImagesAsync(List<HotelImage> images);
    Task<List<HotelImage>> GetHotelImagesAsync(int hotelId);
}