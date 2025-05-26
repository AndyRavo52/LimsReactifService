using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using LimsReactifService.Models;

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
        Task<double> GetCurrentStockAsync(int reactifId); // Nouvelle méthode
        Task<ResteStock> GetResteStockAsync(ResteStockDto resteStockDto);
        // Nouvelle méthode pour récupérer l'état de stock par mois

        Task<Dictionary<string, double>> GetStockByMonthAsync(int idReactif, int year);
        Task<ICollection<ResteStock>> GetResteStockGlobal(DateTime date);
    }
}