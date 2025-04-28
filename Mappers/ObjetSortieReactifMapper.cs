using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class ObjetSortieReactifMapper
    {
        // Convertit une entité ObjetSortieReactif en ObjetSortieReactifDto
        public static ObjetSortieReactifDto ToDto(ObjetSortieReactif objetSortieReactif)
        {
            return new ObjetSortieReactifDto
            {
                IdObjetSortieReactif = objetSortieReactif.IdObjetSortieReactif,
                Designation = objetSortieReactif.Designation ?? string.Empty
            };
        }

        // Convertit un ObjetSortieReactifDto en entité ObjetSortieReactif
        public static ObjetSortieReactif ToEntity(ObjetSortieReactifDto objetSortieReactifDto)
        {
            return new ObjetSortieReactif
            {
                IdObjetSortieReactif = objetSortieReactifDto.IdObjetSortieReactif,
                Designation = objetSortieReactifDto.Designation
            };
        }
    }
}