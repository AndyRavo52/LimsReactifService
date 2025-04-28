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
    public class DepartementService : IDepartementService
    {
        private readonly ReactifServiceContext _context;

        // Constructeur : injection de dépendance du DbContext
        public DepartementService(ReactifServiceContext context)
        {
            _context = context;
        }

        // Compte le nombre total de départements
        public async Task<int> CountDepartementsAsync()
        {
            return await _context.Departement.CountAsync();
        }

        // Récupère une liste paginée de départements
        public async Task<IEnumerable<DepartementDto>> GetDepartementsAsync(int pageIndex, int pageSize)
        {
            var departements = await _context.Departement
                .OrderBy(d => d.Designation)  // Tri par désignation
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return departements.Select(DepartementMapper.ToDto);
        }

        // Récupère un département par son ID
        public async Task<DepartementDto> GetDepartementByIdAsync(int id)
        {
            var departement = await _context.Departement
                .FirstOrDefaultAsync(d => d.IdDepartement == id);

            if (departement == null)
            {
                throw new Exception("Département non trouvé");
            }

            return DepartementMapper.ToDto(departement);
        }
    }
}