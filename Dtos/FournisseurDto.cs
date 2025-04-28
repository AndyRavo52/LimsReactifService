﻿using System.Text.Json.Serialization;

namespace LimsReactifService.Dtos
{
    public class FournisseurDto
    {
        [JsonPropertyName("idFournisseur")]
        public int IdFournisseur { get; set; }

        [JsonPropertyName("designation")]
        public required string Designation { get; set; }
    }
}
