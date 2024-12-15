using System.Text.Json.Serialization;

public class UpdateUserRequestDto
{
    [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("LastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("Purok")]
    public string? Purok { get; set; }

    [JsonPropertyName("BarangayLoc")]
    public string? BarangayLoc { get; set; }

    [JsonPropertyName("CityMunicipality")]
    public string? CityMunicipality { get; set; }

    [JsonPropertyName("Province")]
    public string? Province { get; set; }

    [JsonPropertyName("IdType")]
    public string? IdType { get; set; }

    [JsonPropertyName("Date")]
    public DateTime? Date { get; set; }

    [JsonPropertyName("PhysicalIdNumber")]
    public string? PhysicalIdNumber { get; set; }

    [JsonPropertyName("Email")]
    public string? Email { get; set; }

    [JsonPropertyName("FullAddress")]
    public string? FullAddress => $"{Purok}, {BarangayLoc}, {CityMunicipality}, {Province}";
}
