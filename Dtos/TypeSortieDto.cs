using System.Text.Json.Serialization;

namespace LimsReactifService.Dtos
{
    public class TypeSortieDto
    {
        [JsonPropertyName("idTypeSortie")]
        public int IdTypeSortie { get; set; }

        [JsonPropertyName("designation")]
        public  string? Designation { get; set; }
    }
}
