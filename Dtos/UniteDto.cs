using System.Text.Json.Serialization;

namespace LimsReactifService.Dtos
{
    public class UniteDto
    {
        [JsonPropertyName("idUnite")]
        public int IdUnite { get; set; }

        [JsonPropertyName("designation")]
        public required  string Designation { get; set; }
    }
}
