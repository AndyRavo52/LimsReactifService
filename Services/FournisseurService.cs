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
    public class FournisseurService : IFournisseurService
    {
        private readonly ReactifServiceContext _context;

        // Constructeur : injection de dépendance du DbContext
        public FournisseurService(ReactifServiceContext context)
        {
            _context = context;
        }

        // Compte le nombre total de fournisseurs
        public async Task<int> CountFournisseursAsync()
        {
            return await _context.Fournisseurs.CountAsync();
        }

        // Récupère une liste paginée de fournisseurs
        public async Task<IEnumerable<FournisseurDto>> GetFournisseursAsync(int pageIndex, int pageSize)
        {
            var fournisseurs = await _context.Fournisseurs
                .OrderBy(f => f.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return fournisseurs.Select(FournisseurMapper.ToDto);
        }

        // Récupère un fournisseur par son ID
        public async Task<FournisseurDto> GetFournisseurByIdAsync(int id)
        {
            var fournisseur = await _context.Fournisseurs
                .FirstOrDefaultAsync(f => f.IdFournisseur == id);

            if (fournisseur == null)
            {
                throw new Exception("Fournisseur non trouvé");
            }

            return FournisseurMapper.ToDto(fournisseur);
        }
    }
}
