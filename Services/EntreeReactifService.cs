using LimsReactifService.Data;
using LimsReactifService.Dtos;
using LimsReactifService.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public class EntreeReactifService : IEntreeReactifService
    {
        private readonly ReactifServiceContext _context;

        public EntreeReactifService(ReactifServiceContext context)
        {
            _context = context;
        }

        public async Task<int> CountEntreeReactifsAsync()
        {
            return await _context.EntreeReactifs.CountAsync();
        }

        public async Task<IEnumerable<EntreeReactifDto>> GetEntreeReactifsAsync(int pageIndex, int pageSize)
        {
            var entreeReactifs = await _context.EntreeReactifs
                .Include(er => er.Reactif)
                    .ThenInclude(r => r.Unite)
                .Include(er => er.Fournisseur)
                .OrderByDescending(er => er.DateEntree)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entreeReactifs.Select(er => EntreeReactifMapper.ToDto(er!));
        }

        public async Task<EntreeReactifDto> GetEntreeReactifByIdAsync(int id)
        {
            var entreeReactif = await _context.EntreeReactifs
                .Include(er => er.Reactif)
                    .ThenInclude(r => r.Unite)
                .Include(er => er.Fournisseur)
                .FirstOrDefaultAsync(er => er.IdEntreeReactif == id);

            if (entreeReactif == null)
            {
                throw new Exception("Entrée réactif non trouvée");
            }

            return EntreeReactifMapper.ToDto(entreeReactif!);
        }

        public async Task<EntreeReactifDto> CreateEntreeReactifAsync(EntreeReactifDto entreeReactifDto)
        {
            var entreeReactif = EntreeReactifMapper.ToEntity(entreeReactifDto);
            _context.EntreeReactifs.Add(entreeReactif);
            await _context.SaveChangesAsync();

            return EntreeReactifMapper.ToDto(entreeReactif);
        }

        public async Task<Dictionary<string, decimal>> GetDepensesParMoisAsync(int annee)
        {
            return await _context.EntreeReactifs
                .Where(er => er.DateEntree.Year == annee)
                .GroupBy(er => new { er.DateEntree.Year, er.DateEntree.Month })
                .Select(g => new
                {
                    Periode = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Total = g.Sum(er => er.PrixAchat)
                })
                .ToDictionaryAsync(x => x.Periode, x => x.Total);
        }

        public async Task<Dictionary<int, decimal>> GetDepensesParAnneeAsync()
        {
            return await _context.EntreeReactifs
                .GroupBy(er => er.DateEntree.Year)
                .Select(g => new
                {
                    Annee = g.Key,
                    Total = g.Sum(er => er.PrixAchat)
                })
                .ToDictionaryAsync(x => x.Annee, x => x.Total);
        }
    }
}