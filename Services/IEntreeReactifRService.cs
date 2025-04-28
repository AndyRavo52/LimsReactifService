using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IEntreeReactifService
    {
        // Compte le nombre total d'entrées de réactifs
        Task<int> CountEntreeReactifsAsync();

        // Récupère une liste paginée d'entrées de réactifs
        Task<IEnumerable<EntreeReactifDto>> GetEntreeReactifsAsync(int pageIndex, int pageSize);

        // Récupère une entrée de réactif par son ID
        Task<EntreeReactifDto> GetEntreeReactifByIdAsync(int id);

        // Crée une nouvelle entrée de réactif
        Task<EntreeReactifDto> CreateEntreeReactifAsync(EntreeReactifDto entreeReactifDto);

        

        
    }
}
