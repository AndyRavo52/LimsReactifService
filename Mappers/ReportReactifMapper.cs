using LimsReactifService.Dtos;
using LimsReactifService.Models;

namespace LimsReactifService.Mappers
{
    public static class ReportReactifMapper
    {
        public static ReportReactifDto ToDto(ReportReactif reportReactif)
        {
            return new ReportReactifDto
            {
                IdReportReactif = reportReactif.IdReportReactif,
                DateReport = reportReactif.DateReport,
                Quantite = reportReactif.Quantite,
                IdReactif = reportReactif.IdReactif,
                Reactif = reportReactif.Reactif != null ? ReactifMapper.ToDto(reportReactif.Reactif) : null
            };
        }

        public static ReportReactif ToEntity(ReportReactifDto reportReactifDto)
        {
            return new ReportReactif
            {
                IdReportReactif = reportReactifDto.IdReportReactif,
                DateReport = reportReactifDto.DateReport,
                Quantite = reportReactifDto.Quantite,
                IdReactif = reportReactifDto.IdReactif
            };
        }
    }
}