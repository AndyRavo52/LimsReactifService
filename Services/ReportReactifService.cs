using LimsReactifService.Data;
using LimsReactifService.Dtos;
using LimsReactifService.Mappers;
using LimsReactifService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public class ReportReactifService : IReportReactifService
    {
        private readonly ReactifServiceContext _context;

        public ReportReactifService(ReactifServiceContext context)
        {
            _context = context;
        }

        public async Task<int> CountReportReactifsAsync()
        {
            return await _context.ReportReactif.CountAsync();
        }

        public async Task<IEnumerable<ReportReactifDto>> GetReportReactifsAsync(int pageIndex, int pageSize)
        {
            var reportReactifs = await _context.ReportReactif
                .Include(rr => rr.Reactif)
                .ThenInclude(r => r.Unite) // Inclure l'unité
                .OrderByDescending(rr => rr.DateReport) // Tri par DateReport décroissant
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return reportReactifs.Select(ReportReactifMapper.ToDto);
        }

        public async Task<ReportReactifDto> GetReportReactifByIdAsync(int id)
        {
            var reportReactif = await _context.ReportReactif
                .Include(rr => rr.Reactif)
                .FirstOrDefaultAsync(rr => rr.IdReportReactif == id);

            if (reportReactif == null)
            {
                throw new Exception("Rapport réactif non trouvé");
            }

            return ReportReactifMapper.ToDto(reportReactif);
        }

        public async Task<ReportReactifDto> CreateReportReactifAsync(ReportReactifDto reportReactifDto)
        {
            var reportReactif = ReportReactifMapper.ToEntity(reportReactifDto);
            _context.ReportReactif.Add(reportReactif);
            await _context.SaveChangesAsync();

            return ReportReactifMapper.ToDto(reportReactif);
        }

        
    }
}