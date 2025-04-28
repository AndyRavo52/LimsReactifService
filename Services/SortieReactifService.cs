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
    public class SortieReactifService : ISortieReactifService
    {
        private readonly ReactifServiceContext _context;

        public SortieReactifService(ReactifServiceContext context)
        {
            _context = context;
        }

        public async Task<int> CountSortieReactifsAsync()
        {
            return await _context.SortieReactif.CountAsync();
        }

        public async Task<IEnumerable<SortieReactifDto>> GetSortieReactifsAsync(int pageIndex, int pageSize)
        {
            var sortieReactifs = await _context.SortieReactif
                .Include(sr => sr.Departement)
                .Include(sr => sr.Reactif)
                    .ThenInclude(r => r.Unite)
                .Include(sr => sr.ObjetSortieReactif)
                .OrderByDescending(sr => sr.DateSortie) // Tri par DateSortie décroissant
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return sortieReactifs.Select(SortieReactifMapper.ToDto);
        }

        public async Task<SortieReactifDto> GetSortieReactifByIdAsync(int id)
        {
            var sortieReactif = await _context.SortieReactif
                .Include(sr => sr.Departement)
                .Include(sr => sr.Reactif)
                .Include(sr => sr.ObjetSortieReactif)
                .FirstOrDefaultAsync(sr => sr.IdSortie == id);

            if (sortieReactif == null)
            {
                throw new Exception("Sortie réactif non trouvée");
            }

            return SortieReactifMapper.ToDto(sortieReactif);
        }

        public async Task<SortieReactifDto> CreateSortieReactifAsync(SortieReactifDto sortieReactifDto)
        {
            var sortieReactif = SortieReactifMapper.ToEntity(sortieReactifDto);
            _context.SortieReactif.Add(sortieReactif);
            await _context.SaveChangesAsync();

            return SortieReactifMapper.ToDto(sortieReactif);
        }

    }
}