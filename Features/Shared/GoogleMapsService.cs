using System.Text.Json;
using TeloApi.Features.Hotel.DTOs;

public class GoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        // Intenta obtener la API key de .env o de appsettings.json
        _apiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY") 
                  ?? configuration["GoogleMaps:ApiKey"];

        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new Exception("La API Key de Google Maps no está configurada correctamente.");
        }
    }
    
    public async Task<(string City, string District, string Street)> GetAddressFromCoordinates(decimal latitude, decimal longitude)
    {
        string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={_apiKey}";

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

    public async Task<double?> GetDrivingDistanceAsync(decimal originLat, decimal originLng, decimal destLat, decimal destLng)
    {
        string url = $"https://routes.googleapis.com/directions/v2:computeRoutes";

        var requestBody = new
        {
            origin = new { location = new { latLng = new { latitude = originLat, longitude = originLng } } },
            destination = new { location = new { latLng = new { latitude = destLat, longitude = destLng } } },
            travelMode = "DRIVE", // Viaje en auto sin tráfico
            routingPreference = "TRAFFIC_UNAWARE", // No considerar tráfico
            computeAlternativeRoutes = false
        };

        var requestJson = JsonSerializer.Serialize(requestBody);
        var requestContent = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");
        requestContent.Headers.Add("X-Goog-Api-Key", _apiKey);
        requestContent.Headers.Add("X-Goog-FieldMask", "routes.distanceMeters");

        var response = await _httpClient.PostAsync(url, requestContent);
        if (!response.IsSuccessStatusCode) return null;

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var googleResponse = JsonSerializer.Deserialize<GoogleMapsRoutesResponse>(jsonResponse, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var distanceInMeters = googleResponse?.Routes?[0]?.DistanceMeters;
        return distanceInMeters.HasValue ? distanceInMeters.Value / 1000.0 : null; // Convertimos metros a km
    }
}

// Modelo para deserializar la respuesta de Google
public class GoogleMapsRoutesResponse
{
    public List<Route> Routes { get; set; }
}

public class Route
{
    public int DistanceMeters { get; set; } // La distancia en metros
}
