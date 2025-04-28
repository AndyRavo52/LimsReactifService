using System;
using System.Text.Json.Serialization;

namespace LimsReactifService.Dtos
{
    public class SortieReactifDto
    {
        [JsonPropertyName("idSortie")]
        public int IdSortie { get; set; }

        [JsonPropertyName("quantite")]
        public double Quantite { get; set; }

        [JsonPropertyName("dateSortie")]
        public DateTime DateSortie { get; set; }

        [JsonPropertyName("idDepartement")]
        public int? IdDepartement { get; set; }

        [JsonPropertyName("departement")]
        public DepartementDto? Departement { get; set; }

        [JsonPropertyName("idReactif")]
        public int IdReactif { get; set; }

        [JsonPropertyName("reactif")]
        public ReactifDto? Reactif { get; set; }

        [JsonPropertyName("idObjetSortieReactif")]
        public int IdObjetSortieReactif { get; set; }

        [JsonPropertyName("objetSortieReactif")]
        public ObjetSortieReactifDto? ObjetSortieReactif { get; set; }
    }
}