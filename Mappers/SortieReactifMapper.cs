using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class SortieReactifMapper
    {
        public static SortieReactifDto ToDto(SortieReactif sortieReactif)
        {
            return new SortieReactifDto
            {
                IdSortie = sortieReactif.IdSortie,
                Quantite = sortieReactif.Quantite,
                DateSortie = sortieReactif.DateSortie,
                IdDepartement = sortieReactif.IdDepartement,
                Departement = sortieReactif.Departement != null ? DepartementMapper.ToDto(sortieReactif.Departement) : null,
                IdReactif = sortieReactif.IdReactif,
                Reactif = sortieReactif.Reactif != null ? ReactifMapper.ToDto(sortieReactif.Reactif) : null,
                IdObjetSortieReactif = sortieReactif.IdObjetSortieReactif,
                ObjetSortieReactif = sortieReactif.ObjetSortieReactif != null ? ObjetSortieReactifMapper.ToDto(sortieReactif.ObjetSortieReactif) : null
            };
        }

        public static SortieReactif ToEntity(SortieReactifDto sortieReactifDto)
        {
            return new SortieReactif
            {
                IdSortie = sortieReactifDto.IdSortie,
                Quantite = sortieReactifDto.Quantite,
                DateSortie = sortieReactifDto.DateSortie,
                IdDepartement = sortieReactifDto.IdDepartement,
                IdReactif = sortieReactifDto.IdReactif,
                IdObjetSortieReactif = sortieReactifDto.IdObjetSortieReactif
            };
        }
    }
}