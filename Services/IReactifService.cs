using LimsReactifService.Models;
using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IReactifService
    {
        Task<int> CountReactifsAsync();
        Task<IEnumerable<ReactifDto>> GetReactifsAsync(int pageIndex, int pageSize);
        Task<ReactifDto> GetReactifByIdAsync(int id);
        Task<ReactifDto> CreateReactifAsync(ReactifDto reactifDto);
        Task<ReactifDto> UpdateReactifAsync(int id, ReactifDto reactifDto);
        Task<bool> DeleteReactifAsync(int id);
        Task<IEnumerable<ReactifDto>> SearchReactifsAsync(string searchTerm);

        Task<Dictionary<string, double>> GetStockEvolutionAsync(int reactifId, int year);

        Task<ResteStock> GetResteStockAsync(ResteStockDto resteStockDto);


        Task<double> GetCurrentStockAsync(int reactifId); // Nouvelle méthode
    }
}