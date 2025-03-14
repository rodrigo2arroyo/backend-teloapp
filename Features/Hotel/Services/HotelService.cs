using System.Diagnostics;
using System.Text.Json;
using TeloApi.Features.Hotel.DTOs;
using TeloApi.Features.Hotel.Repositories;
using TeloApi.Shared;
using DotNetEnv;

namespace TeloApi.Features.Hotel.Services;
using Models;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    private async Task<(string City, string District, string Street)> GetAddressFromCoordinates(decimal latitude, decimal longitude)
    {
        Env.Load();
        string apiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY");
        if (string.IsNullOrEmpty(apiKey)) throw new Exception("Google Maps API Key is missing. Make sure it's set in the .env file.");
        
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<GoogleGeocodingResponse>(response);

            if (data != null && data.Results.Any())
            {
                var bestResult = data.Results.FirstOrDefault();
                var addressComponents = bestResult.AddressComponents;

                string city = "";
                string district = "";
                string streetName = "";
                string streetNumber = "";

                // Recorrer todos los addressComponents para obtener cada dato
                foreach (var component in addressComponents)
                {
                    if (component.Types.Contains("locality"))
                    {
                        district = component.LongName; // Ciudad
                    }
                    else if (component.Types.Contains("administrative_area_level_2"))
                    {
                        city = component.LongName; // Distrito
                    }
                    else if (component.Types.Contains("route"))
                    {
                        streetName = component.LongName; // Nombre de la calle
                    }
                    else if (component.Types.Contains("street_number"))
                    {
                        streetNumber = component.LongName; // Número de la calle
                    }
                }

                // Concatenar Calle + Número (si existe número)
                string street = !string.IsNullOrEmpty(streetNumber) ? $"{streetName} {streetNumber}" : streetName;

                return (city, district, street);
            }
        }

        return ("", "", "");
    }
    public async Task<GenericResponse> CreateHotelAsync(CreateHotel request)
    {
        var (city, district, street) = await GetAddressFromCoordinates(request.Location.Latitude, request.Location.Longitude);

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
        if (hotel == null)
            return new GenericResponse { Message = "Hotel not found." };

        hotel.Name = request.Name;
        hotel.UpdatedBy = request.UpdatedBy;

        var updatedHotel = await hotelRepository.UpdateHotelAsync(hotel);

        return new GenericResponse { Id = updatedHotel.Id, Message = "Hotel updated successfully." };
    }

    public async Task<HotelResponse> GetHotelByIdAsync(int hotelId)
    {
        var hotel = await hotelRepository.GetHotelByIdWithDetailsAsync(hotelId);
        Trace.WriteLine(hotel);
        if (hotel == null) return null;

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

        var hotelResponses = hotels.Select(h => new HotelResponse
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description!,
            Location = new LocationResponse
            {
                City = h.Location.City,
                District = h.Location.District,
                Street = h.Location.Street,
                Latitude = h.Location.Latitude,
                Longitude = h.Location.Longitude,
            },
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
                CountryCode = c.CountryCode??  "",
                Email = c.Email?? "",
            }).ToList()
        }).ToList();

        return new HotelsResult
        {
            Hotels = hotelResponses,
            TotalCount = totalCount
        };
    }
}
