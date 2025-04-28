using LimsReactifService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public interface IReportReactifService
    {
        // Compte le nombre total de rapports de réactifs
        Task<int> CountReportReactifsAsync();

        // Récupère une liste paginée de rapports de réactifs
        Task<IEnumerable<ReportReactifDto>> GetReportReactifsAsync(int pageIndex, int pageSize);

        // Récupère un report de réactif par son ID
        Task<ReportReactifDto> GetReportReactifByIdAsync(int id);

        // Crée un nouveau report de réactif
        Task<ReportReactifDto> CreateReportReactifAsync(ReportReactifDto reportReactifDto);

       
        
    }
}