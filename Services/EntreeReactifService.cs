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
                .OrderByDescending(er => er.DateEntree) // Tri par DateEntree décroissant
                // .OrderBy(er => er.DateEntree)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entreeReactifs.Select(EntreeReactifMapper.ToDto);
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

            return EntreeReactifMapper.ToDto(entreeReactif);
        }

        public async Task<EntreeReactifDto> CreateEntreeReactifAsync(EntreeReactifDto entreeReactifDto)
        {
            var entreeReactif = EntreeReactifMapper.ToEntity(entreeReactifDto);
            _context.EntreeReactifs.Add(entreeReactif);
            await _context.SaveChangesAsync();

            return EntreeReactifMapper.ToDto(entreeReactif);
        }

        

        
    }
}