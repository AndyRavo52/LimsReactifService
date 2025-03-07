using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class UniteMapper
    {
        // Convertit une entité Unite en UniteDto
        public static UniteDto ToDto(Unite unite)
        {
            return new UniteDto
            {
                IdUnite = unite.IdUnite,
                Designation = unite.Designation ?? string.Empty
            };
        }

        // Convertit un UniteDto en entité Unite
        public static Unite ToEntity(UniteDto uniteDto)
        {
            return new Unite
            {
                IdUnite = uniteDto.IdUnite,
                Designation = uniteDto.Designation
            };
        }
    }
}
