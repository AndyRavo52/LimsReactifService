using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface ISortieReactifService
    {
        // Compte le nombre total de sorties de réactifs
        Task<int> CountSortieReactifsAsync();

        // Récupère une liste paginée de sorties de réactifs
        Task<IEnumerable<SortieReactifDto>> GetSortieReactifsAsync(int pageIndex, int pageSize);

        // Récupère une sortie de réactif par son ID
        Task<SortieReactifDto> GetSortieReactifByIdAsync(int id);

        // Crée une nouvelle sortie de réactif
        Task<SortieReactifDto> CreateSortieReactifAsync(SortieReactifDto sortieReactifDto);

       

       
    }
}