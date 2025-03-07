using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class TypeSortieMapper
    {
        // Convertit une entité TypeSortie en TypeSortieDto
        public static TypeSortieDto ToDto(TypeSortie typeSortie)
        {
            return new TypeSortieDto
            {
                IdTypeSortie = typeSortie.IdTypeSortie,
                Designation = typeSortie.Designation ?? string.Empty
            };
        }

        // Convertit un TypeSortieDto en entité TypeSortie
        public static TypeSortie ToEntity(TypeSortieDto typeSortieDto)
        {
            return new TypeSortie
            {
                IdTypeSortie = typeSortieDto.IdTypeSortie,
                Designation = typeSortieDto.Designation
            };
        }
    }
}
