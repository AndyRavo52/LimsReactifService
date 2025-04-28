using System;
using System.Text.Json.Serialization;

namespace LimsReactifService.Dtos
{
    public class EntreeReactifDto
    {
        [JsonPropertyName("idEntreeReactif")]
        public int IdEntreeReactif { get; set; }

        [JsonPropertyName("quantite")]
        public double Quantite { get; set; }

        [JsonPropertyName("prixAchat")]
        public decimal PrixAchat { get; set; }

        [JsonPropertyName("bonReception")]
        public  string? BonReception { get; set; }

        // Nouveau champ ajouté
        [JsonPropertyName("bonDeCommande")]
        public  string? BonDeCommande { get; set; }

        // Nouveau champ ajouté
        [JsonPropertyName("numeroFacture")]
        public  string? NumeroFacture { get; set; }

        [JsonPropertyName("dateEntree")]
        public DateTime DateEntree { get; set; }

        [JsonPropertyName("datePeremption")]
        public DateTime? DatePeremption { get; set; }

        [JsonPropertyName("idReactif")]
        public int? IdReactif { get; set; }

        [JsonPropertyName("reactif")]
        public ReactifDto? Reactif { get; set; }

        [JsonPropertyName("idFournisseur")]
        public int IdFournisseur { get; set; }

        [JsonPropertyName("fournisseur")]
        public FournisseurDto? Fournisseur { get; set; }
    }
}