using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class FournisseurMapper
    {
        // Convertit une entité Fournisseur en FournisseurDto
        public static FournisseurDto ToDto(Fournisseur fournisseur)
        {
            return new FournisseurDto
            {
                IdFournisseur = fournisseur.IdFournisseur,
                Designation = fournisseur.Designation ?? string.Empty
            };
        }

        // Convertit un FournisseurDto en entité Fournisseur
        public static Fournisseur ToEntity(FournisseurDto fournisseurDto)
        {
            return new Fournisseur
            {
                IdFournisseur = fournisseurDto.IdFournisseur,
                Designation = fournisseurDto.Designation
            };
        }
    }
}