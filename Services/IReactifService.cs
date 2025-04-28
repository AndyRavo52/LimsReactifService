using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IReactifService
    {
        // Compte le nombre total de réactifs
        Task<int> CountReactifsAsync();

        // Récupère une liste paginée de réactifs
        Task<IEnumerable<ReactifDto>> GetReactifsAsync(int pageIndex, int pageSize);

        // Récupère un réactif par son ID
        Task<ReactifDto> GetReactifByIdAsync(int id);

        // Crée un nouveau réactif
        Task<ReactifDto> CreateReactifAsync(ReactifDto reactifDto);

        // Met à jour un réactif existant
        Task<ReactifDto> UpdateReactifAsync(int id, ReactifDto reactifDto);

        // Supprime un réactif
        Task<bool> DeleteReactifAsync(int id);

        // Recherche des réactifs en fonction d'un terme (sur la désignation)
        Task<IEnumerable<ReactifDto>> SearchReactifsAsync(string searchTerm);
    }
}
