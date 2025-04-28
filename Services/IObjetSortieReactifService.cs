using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IObjetSortieReactifService
    {
        // Compte le nombre total d'objets de sortie réactif
        Task<int> CountObjetsSortieReactifAsync();

        // Récupère une liste paginée d'objets de sortie réactif
        Task<IEnumerable<ObjetSortieReactifDto>> GetObjetsSortieReactifAsync(int pageIndex, int pageSize);

        // Récupère un objet de sortie réactif par son ID
        Task<ObjetSortieReactifDto> GetObjetSortieReactifByIdAsync(int id);

        // Crée un nouvel objet de sortie réactif
        Task<ObjetSortieReactifDto> CreateObjetSortieReactifAsync(ObjetSortieReactifDto objetSortieReactifDto);

        // Met à jour un objet de sortie réactif existant
        Task<ObjetSortieReactifDto> UpdateObjetSortieReactifAsync(int id, ObjetSortieReactifDto objetSortieReactifDto);

        // Supprime un objet de sortie réactif
        Task<bool> DeleteObjetSortieReactifAsync(int id);
    }
}