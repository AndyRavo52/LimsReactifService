using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class DepartementMapper
    {
        public static DepartementDto ToDto(Departement departement)
        {
            return new DepartementDto
            {
                IdDepartement = departement.IdDepartement,
                Code = departement.Code,
                Designation = departement.Designation
            };
        }

        public static Departement ToEntity(DepartementDto departementDto)
        {
            return new Departement
            {
                IdDepartement = departementDto.IdDepartement,
                Code = departementDto.Code,
                Designation = departementDto.Designation
            };
        }
    }
}