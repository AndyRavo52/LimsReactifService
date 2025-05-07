// using LimsReactifService.Data;
// using LimsReactifService.Dtos;
// using LimsReactifService.Mappers;
// using LimsReactifService.Models;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace LimsReactifService.Services
// {
//     public class ReactifService : IReactifService
//     {
//         private readonly ReactifServiceContext _context;

//         // Constructeur : injection de dépendance du DbContext
//         public ReactifService(ReactifServiceContext context)
//         {
//             _context = context;
//         }

//         // Compte le nombre total de réactifs
//         public async Task<int> CountReactifsAsync()
//         {
//             return await _context.Reactifs.CountAsync();
//         }

//         // Récupère une liste paginée de réactifs
//         public async Task<IEnumerable<ReactifDto>> GetReactifsAsync(int pageIndex, int pageSize)
//         {
//             var reactifs = await _context.Reactifs
//                 .OrderBy(r => r.Designation)
//                 .Skip((pageIndex - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToListAsync();

//             return reactifs.Select(ReactifMapper.ToDto);
//         }

//         // Récupère un réactif par son ID
//         public async Task<ReactifDto> GetReactifByIdAsync(int id)
//         {
//             var reactif = await _context.Reactifs
//                 .Include(r => r.TypeSortie)
//                 .Include(r => r.Unite)
//                 .FirstOrDefaultAsync(r => r.IdReactif == id);

//             if (reactif == null)
//             {
//                 throw new Exception("Réactif non trouvé");
//             }

//             return ReactifMapper.ToDto(reactif);
//         }

//         // Crée un nouveau réactif
//         public async Task<ReactifDto> CreateReactifAsync(ReactifDto reactifDto)
//         {
//             var reactif = ReactifMapper.ToEntity(reactifDto);
//             _context.Reactifs.Add(reactif);
//             await _context.SaveChangesAsync();

//             return ReactifMapper.ToDto(reactif);
//         }

//         // Met à jour un réactif existant
//         public async Task<ReactifDto> UpdateReactifAsync(int id, ReactifDto reactifDto)
//         {
//             var reactif = await _context.Reactifs.FirstOrDefaultAsync(r => r.IdReactif == id);

//             if (reactif == null)
//             {
//                 throw new Exception("Réactif non trouvé");
//             }

//             reactif.Designation = reactifDto.Designation;
//             reactif.IdTypeSortie = reactifDto.IdTypeSortie;
//             reactif.IdUnite = reactifDto.IdUnite;

//             await _context.SaveChangesAsync();

//             return ReactifMapper.ToDto(reactif);
//         }

//         // Supprime un réactif
//         public async Task<bool> DeleteReactifAsync(int id)
//         {
//             var reactif = await _context.Reactifs.FirstOrDefaultAsync(r => r.IdReactif == id);

//             if (reactif == null)
//             {
//                 return false;
//             }

//             _context.Reactifs.Remove(reactif);
//             await _context.SaveChangesAsync();

//             return true;
//         }

//         // Recherche des réactifs en fonction d'un terme (sur la désignation)
//         public async Task<IEnumerable<ReactifDto>> SearchReactifsAsync(string searchTerm)
//         {
//             var query = _context.Reactifs
//             .Include(r => r.Unite)
//             .AsQueryable();

//             if (!string.IsNullOrWhiteSpace(searchTerm))
//             {
//                 query = query.Where(r => EF.Functions.Like(r.Designation, $"%{searchTerm}%"));
//             }

//             var reactifs = await query
//                 .OrderBy(r => r.Designation)
//                 .ToListAsync();

//             return reactifs.Select(ReactifMapper.ToDto);
//         }
//     }
// }


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
    public class ReactifService : IReactifService
    {
        private readonly ReactifServiceContext _context;

        public ReactifService(ReactifServiceContext context)
        {
            _context = context;
        }

        public async Task<int> CountReactifsAsync()
        {
            return await _context.Reactifs.CountAsync();
        }

        public async Task<IEnumerable<ReactifDto>> GetReactifsAsync(int pageIndex, int pageSize)
        {
            var reactifs = await _context.Reactifs
                .OrderBy(r => r.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return reactifs.Select(ReactifMapper.ToDto);
        }

        public async Task<ReactifDto> GetReactifByIdAsync(int id)
        {
            var reactif = await _context.Reactifs
                .Include(r => r.TypeSortie)
                .Include(r => r.Unite)
                .FirstOrDefaultAsync(r => r.IdReactif == id);

            if (reactif == null)
            {
                throw new Exception("Réactif non trouvé");
            }

            return ReactifMapper.ToDto(reactif);
        }

        public async Task<ReactifDto> CreateReactifAsync(ReactifDto reactifDto)
        {
            var reactif = ReactifMapper.ToEntity(reactifDto);
            _context.Reactifs.Add(reactif);
            await _context.SaveChangesAsync();

            return ReactifMapper.ToDto(reactif);
        }

        public async Task<ReactifDto> UpdateReactifAsync(int id, ReactifDto reactifDto)
        {
            var reactif = await _context.Reactifs.FirstOrDefaultAsync(r => r.IdReactif == id);

            if (reactif == null)
            {
                throw new Exception("Réactif non trouvé");
            }

            reactif.Designation = reactifDto.Designation;
            reactif.IdTypeSortie = reactifDto.IdTypeSortie;
            reactif.IdUnite = reactifDto.IdUnite;

            await _context.SaveChangesAsync();

            return ReactifMapper.ToDto(reactif);
        }

        public async Task<bool> DeleteReactifAsync(int id)
        {
            var reactif = await _context.Reactifs.FirstOrDefaultAsync(r => r.IdReactif == id);

            if (reactif == null)
            {
                return false;
            }

            _context.Reactifs.Remove(reactif);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ReactifDto>> SearchReactifsAsync(string searchTerm)
        {
            var query = _context.Reactifs
                .Include(r => r.Unite)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r => EF.Functions.Like(r.Designation, $"%{searchTerm}%"));
            }

            var reactifs = await query
                .OrderBy(r => r.Designation)
                .ToListAsync();

            return reactifs.Select(ReactifMapper.ToDto);
        }

        public async Task<Dictionary<string, double>> GetStockByMonthAsync(int idReactif, int year)
        {
            var stockByMonth = new Dictionary<string, double>
            {
                { "J", 0 }, { "F", 0 }, { "M", 0 }, { "A", 0 },
                { "M", 0 }, { "J", 0 }, { "J", 0 }, { "A", 0 },
                { "S", 0 }, { "O", 0 }, { "N", 0 }, { "D", 0 }
            };

            var entrees = await _context.EntreeReactifs
                .Where(e => e.IdReactif == idReactif && e.DateEntree.Year == year)
                .ToListAsync();

            var sorties = await _context.SortieReactif
                .Where(s => s.IdReactif == idReactif && s.DateSortie.Year == year)
                .ToListAsync();

            var months = new[] { "J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D" };
            for (int month = 1; month <= 12; month++)
            {
                double totalEntrees = entrees
                    .Where(e => e.DateEntree.Month <= month)
                    .Sum(e => e.Quantite);

                double totalSorties = sorties
                    .Where(s => s.DateSortie.Month <= month)
                    .Sum(s => s.Quantite);

                double stock = totalEntrees - totalSorties;
                stockByMonth[months[month - 1]] = stock > 0 ? stock : 0;
            }

            return stockByMonth;
        }
    }
}