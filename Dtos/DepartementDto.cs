using System.Text.Json.Serialization;

namespace LimsReactifService.Dtos
{
    public class DepartementDto
    {
        [JsonPropertyName("idDepartement")]
        public int IdDepartement { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("designation")]
        public string? Designation { get; set; }
    }
}