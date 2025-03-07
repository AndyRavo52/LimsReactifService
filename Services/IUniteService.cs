using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IUniteService
    {
        // Compte le nombre total d'unités
        Task<int> CountUnitesAsync();

        // Récupère une liste paginée d'unités
        Task<IEnumerable<UniteDto>> GetUnitesAsync(int pageIndex, int pageSize);

        // Récupère une unité par son ID
        Task<UniteDto> GetUniteByIdAsync(int id);

        // Crée une nouvelle unité
        Task<UniteDto> CreateUniteAsync(UniteDto uniteDto);

        // Met à jour une unité existante
        Task<UniteDto> UpdateUniteAsync(int id, UniteDto uniteDto);

        // Supprime une unité
        Task<bool> DeleteUniteAsync(int id);
    }
}
