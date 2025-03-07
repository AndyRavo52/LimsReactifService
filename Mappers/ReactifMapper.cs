using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class ReactifMapper
    {
        // Convertit une entité Reactif en ReactifDto
        public static ReactifDto ToDto(Reactif reactif)
        {
            return new ReactifDto
            {
                IdReactif = reactif.IdReactif,
                Designation = reactif.Designation,
                IdTypeSortie = reactif.IdTypeSortie,
                TypeSortie = reactif.TypeSortie != null ? new TypeSortieDto
                {
                    // Ici, mappez les propriétés spécifiques de TypeSortie selon votre modèle
                    IdTypeSortie = reactif.TypeSortie.IdTypeSortie,
                    Designation = reactif.TypeSortie.Designation
                } : null,
                IdUnite = reactif.IdUnite,
                Unite = reactif.Unite != null ? new UniteDto
                {
                    // Ici, mappez les propriétés spécifiques de Unite selon votre modèle
                    IdUnite = reactif.Unite.IdUnite,
                    Designation = reactif.Unite.Designation
                } : null
            };
        }

        // Convertit un ReactifDto en entité Reactif
        public static Reactif ToEntity(ReactifDto reactifDto)
        {
            return new Reactif
            {
                IdReactif = reactifDto.IdReactif,
                Designation = reactifDto.Designation,
                IdTypeSortie = reactifDto.IdTypeSortie,
                IdUnite = reactifDto.IdUnite
                // Les propriétés de navigation (TypeSortie, Unite) ne sont généralement pas mappées ici
            };
        }
    }
}
