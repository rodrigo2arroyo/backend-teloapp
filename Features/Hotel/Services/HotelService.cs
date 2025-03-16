using System.Diagnostics;
using System.Text.Json;
using TeloApi.Features.Hotel.DTOs;
using TeloApi.Features.Hotel.Repositories;
using TeloApi.Shared;
using DotNetEnv;

namespace TeloApi.Features.Hotel.Services;
using Models;

public class HotelService(IHotelRepository hotelRepository, GoogleMapsService googleMapsService) : IHotelService
{
    public async Task<GenericResponse> CreateHotelAsync(CreateHotel request)
    {
        var (city, district, street) = await googleMapsService.GetAddressFromCoordinates(request.Location.Latitude, request.Location.Longitude);

        var hotel = new Hotel
        {
            Name = request.Name,
            Description = request.Description,
            Location = new Location
            {
                City = city,
                District = district,
                Street = street,
                Longitude = request.Location.Longitude,
                Latitude = request.Location.Latitude,
            },
            CreatedBy = request.CreatedBy
        };

        var createdHotel = await hotelRepository.CreateHotelAsync(hotel);

        return new GenericResponse { Id = createdHotel.Id, Message = "Hotel created successfully." };
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

        await hotelRepository.AddHotelImagesAsync(uploadedImages);

        return new GenericResponse { Message = "Images uploaded successfully." };
    }
    public async Task<List<string>> GetHotelImagesAsync(int hotelId)
    {
        var images = await hotelRepository.GetHotelImagesAsync(hotelId);
        return images.Select(i => i.ImageUrl).ToList();
    }
    public async Task<GenericResponse> UpdateHotelAsync(UpdateHotel request)
    {
        var hotel = await hotelRepository.GetHotelByIdWithDetailsAsync(request.Id);

        hotel.Name = request.Name;
        hotel.UpdatedBy = request.UpdatedBy;

        var updatedHotel = await hotelRepository.UpdateHotelAsync(hotel);

        return new GenericResponse { Id = updatedHotel.Id, Message = "Hotel updated successfully." };
    }
    public async Task<HotelResponse> GetHotelByIdAsync(int hotelId)
    {
        var hotel = await hotelRepository.GetHotelByIdWithDetailsAsync(hotelId);
        Trace.WriteLine(hotel);

        return new HotelResponse
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Description = hotel.Description!,
            Location = new LocationResponse
            {
                City = hotel.Location!.City,
                District = hotel.Location.District,
                Street = hotel.Location.Street,
                Latitude = hotel.Location.Latitude,
                Longitude = hotel.Location.Longitude,
            },
            Rates = hotel.Rates.Select(r => new RateResponse
            {
                Id = r.Id,
                RateType = r.RateType,
                Description = r.Description?? "",
                Price = r.Price,
                Duration = r.Duration,
                Services = r.ServiceRates.Select(sr => new ServiceResponse
                {
                    Id = sr.Service.Id,
                    Name = sr.Service.Name
                }).ToList()
            }).ToList(),
            Promotions = hotel.Promotions.Select(p => new PromotionResponse
            {
                Id = p.Id,
                RateType = p.RateType,
                Description = p.Description?? "",
                PromotionalPrice = p.PromotionalPrice,
                Duration = p.Duration,
                Services = p.ServicePromotions.Select(sp => new ServiceResponse
                {
                    Id = sp.Service.Id,
                    Name = sp.Service.Name
                }).ToList()
            }).ToList(),
            Reviews = hotel.Reviews.Select(r => new ReviewResponse
            {
                Id = r.Id,
                Author = r.Author?? "",
                Description = r.Description,
                Rating = r.Rating?? 0,
            }).ToList(),
            Images = hotel.HotelImages.Select(img => img.ImageUrl).ToList(),
            Contacts = hotel.Contacts.Select(c => new ContactResponse
            {
                Id = c.Id,
                FirstName = c.Firstname,
                LastName = c.Lastname,
                Phone = c.Phone,
                CountryCode = c.CountryCode??  "",
                Email = c.Email?? "",
            }).ToList()
        };
    }
    public async Task<HotelsResult> ListHotelsAsync(HotelsRequest request)
    {
        var (hotels, totalCount) = await hotelRepository.GetHotelsAsync(request);
        decimal? userLat = request.UserLatitude;
        decimal? userLng = request.UserLongitude;

        var hotelResponses = new List<HotelResponse>();

        foreach (var h in hotels)
        {
            var locationResponse = new LocationResponse
            {
                City = h.Location.City,
                District = h.Location.District,
                Street = h.Location.Street,
                Latitude = h.Location.Latitude,
                Longitude = h.Location.Longitude
            };
            
            if (userLat.HasValue && userLng.HasValue && h.Location.Latitude.HasValue && h.Location.Longitude.HasValue)
            {
                locationResponse.DistanceKm = await googleMapsService.GetDrivingDistanceAsync(
                    userLat.Value, userLng.Value, h.Location.Latitude.Value, h.Location.Longitude.Value
                );
            }

            hotelResponses.Add(new HotelResponse
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description!,
                Location = locationResponse,
                Rates = h.Rates.Select(r => new RateResponse
                {
                    Id = r.Id,
                    RateType = r.RateType,
                    Description = r.Description,
                    Price = r.Price,
                    Duration = r.Duration,
                    Services = r.ServiceRates.Select(sr => new ServiceResponse
                    {
                        Id = sr.Service.Id,
                        Name = sr.Service.Name
                    }).ToList()
                }).ToList(),
                Promotions = h.Promotions.Select(p => new PromotionResponse
                {
                    Id = p.Id,
                    RateType = p.RateType,
                    Description = p.Description,
                    PromotionalPrice = p.PromotionalPrice,
                    Duration = p.Duration,
                    Services = p.ServicePromotions.Select(sp => new ServiceResponse
                    {
                        Id = sp.Service.Id,
                        Name = sp.Service.Name
                    }).ToList()
                }).ToList(),
                Reviews = h.Reviews.Select(r => new ReviewResponse
                {
                    Id = r.Id,
                    Author = r.Author,
                    Description = r.Description,
                    Rating = 0
                }).ToList(),
                Images = h.HotelImages.Select(img => img.ImageUrl).ToList(),
                Contacts = h.Contacts.Select(c => new ContactResponse
                {
                    Id = c.Id,
                    FirstName = c.Firstname,
                    LastName = c.Lastname,
                    Phone = c.Phone,
                    CountryCode = c.CountryCode ?? "",
                    Email = c.Email ?? "",
                }).ToList()
            });
        }

        // Ordenar por cercanía si el usuario proporcionó ubicación
        if (userLat.HasValue && userLng.HasValue)
        {
            hotelResponses = hotelResponses.OrderBy(h => h.Location.DistanceKm).ToList();
        }

        return new HotelsResult
        {
            Hotels = hotelResponses,
            TotalCount = totalCount
        };
    }
}
