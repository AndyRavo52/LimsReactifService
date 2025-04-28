using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class EntreeReactifMapper
    {
        public static EntreeReactifDto ToDto(EntreeReactif entreeReactif)
        {
            return new EntreeReactifDto
            {
                IdEntreeReactif = entreeReactif.IdEntreeReactif,
                Quantite = entreeReactif.Quantite,
                PrixAchat = entreeReactif.PrixAchat,
                BonReception = entreeReactif.BonReception,
                // Ajout des nouveaux champs
                BonDeCommande = entreeReactif.BonDeCommande,
                NumeroFacture = entreeReactif.NumeroFacture,
                DateEntree = entreeReactif.DateEntree,
                DatePeremption = entreeReactif.DatePeremption,
                IdReactif = entreeReactif.IdReactif,
                Reactif = entreeReactif.Reactif != null ? ReactifMapper.ToDto(entreeReactif.Reactif) : null,
                IdFournisseur = entreeReactif.IdFournisseur,
                Fournisseur = entreeReactif.Fournisseur != null ? FournisseurMapper.ToDto(entreeReactif.Fournisseur) : null
            };
        }

        public static EntreeReactif ToEntity(EntreeReactifDto entreeReactifDto)
        {
            return new EntreeReactif
            {
                IdEntreeReactif = entreeReactifDto.IdEntreeReactif,
                Quantite = entreeReactifDto.Quantite,
                PrixAchat = entreeReactifDto.PrixAchat,
                BonReception = entreeReactifDto.BonReception,
                // Ajout des nouveaux champs
                BonDeCommande = entreeReactifDto.BonDeCommande,
                NumeroFacture = entreeReactifDto.NumeroFacture,
                DateEntree = entreeReactifDto.DateEntree,
                DatePeremption = entreeReactifDto.DatePeremption,
                IdReactif = entreeReactifDto.IdReactif,
                IdFournisseur = entreeReactifDto.IdFournisseur
            };
        }
    }
}