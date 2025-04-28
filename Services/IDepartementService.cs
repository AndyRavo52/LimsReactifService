using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IDepartementService
    {
        // Compte le nombre total de départements
        Task<int> CountDepartementsAsync();

        // Récupère une liste paginée de départements
        Task<IEnumerable<DepartementDto>> GetDepartementsAsync(int pageIndex, int pageSize);

        // Récupère un département par son ID
        Task<DepartementDto> GetDepartementByIdAsync(int id);
    }
}