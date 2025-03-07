using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface ITypeSortieService
    {
        // Compte le nombre total de types de sortie
        Task<int> CountTypesSortieAsync();

        // Récupère une liste paginée de types de sortie
        Task<IEnumerable<TypeSortieDto>> GetTypesSortieAsync(int pageIndex, int pageSize);

        // Récupère un type de sortie par son ID
        Task<TypeSortieDto> GetTypeSortieByIdAsync(int id);

        // Crée un nouveau type de sortie
        Task<TypeSortieDto> CreateTypeSortieAsync(TypeSortieDto typeSortieDto);

        // Met à jour un type de sortie existant
        Task<TypeSortieDto> UpdateTypeSortieAsync(int id, TypeSortieDto typeSortieDto);

        // Supprime un type de sortie
        Task<bool> DeleteTypeSortieAsync(int id);
    }
}

