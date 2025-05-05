using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IEntreeReactifService
    {
        Task<int> CountEntreeReactifsAsync();
        Task<IEnumerable<EntreeReactifDto>> GetEntreeReactifsAsync(int pageIndex, int pageSize);
        Task<EntreeReactifDto> GetEntreeReactifByIdAsync(int id);
        Task<EntreeReactifDto> CreateEntreeReactifAsync(EntreeReactifDto entreeReactifDto);
        Task<Dictionary<string, decimal>> GetDepensesParMoisAsync(int annee);
        Task<Dictionary<int, decimal>> GetDepensesParAnneeAsync();
    }
}