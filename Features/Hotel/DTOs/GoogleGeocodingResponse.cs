using System.Text.Json.Serialization;

namespace TeloApi.Features.Hotel.DTOs;

public class GoogleGeocodingResponse
{
    [JsonPropertyName("results")]
    public List<GeocodeResult> Results { get; set; }
}

public class GeocodeResult
{
    [JsonPropertyName("address_components")]
    public List<AddressComponent> AddressComponents { get; set; }
}

public class AddressComponent
{
    [JsonPropertyName("long_name")]
    public string LongName { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }

    [JsonPropertyName("types")]
    public List<string> Types { get; set; }
}