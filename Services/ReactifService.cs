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

        // Constructeur : injection de dépendance du DbContext
        public ReactifService(ReactifServiceContext context)
        {
            _context = context;
        }

        // Compte le nombre total de réactifs
        public async Task<int> CountReactifsAsync()
        {
            return await _context.Reactifs.CountAsync();
        }

        // Récupère une liste paginée de réactifs
        public async Task<IEnumerable<ReactifDto>> GetReactifsAsync(int pageIndex, int pageSize)
        {
            var reactifs = await _context.Reactifs
                .OrderBy(r => r.Designation) // Trie par désignation
                .Skip((pageIndex - 1) * pageSize) // Saute les éléments des pages précédentes
                .Take(pageSize) // Prend un nombre limité d'éléments
                .ToListAsync();

            // Convertit les entités en DTOs
            return reactifs.Select(ReactifMapper.ToDto);
        }

        // Récupère un réactif par son ID
        public async Task<ReactifDto> GetReactifByIdAsync(int id)
        {
            var reactif = await _context.Reactifs
                .Include(r => r.TypeSortie) // Charge la relation avec TypeSortie
                .Include(r => r.Unite) // Charge la relation avec Unite
                .FirstOrDefaultAsync(r => r.IdReactif == id);

            if (reactif == null)
            {
                throw new Exception("Réactif non trouvé");
            }

            // Convertit l'entité en DTO
            return ReactifMapper.ToDto(reactif);
        }

        // Crée un nouveau réactif
        public async Task<ReactifDto> CreateReactifAsync(ReactifDto reactifDto)
        {
            // Convertit le DTO en entité
            var reactif = ReactifMapper.ToEntity(reactifDto);

            // Ajoute le réactif à la base de données
            _context.Reactifs.Add(reactif);
            await _context.SaveChangesAsync();

            // Convertit l'entité en DTO pour la réponse
            return ReactifMapper.ToDto(reactif);
        }

        // Met à jour un réactif existant
        public async Task<ReactifDto> UpdateReactifAsync(int id, ReactifDto reactifDto)
        {
            // Récupère le réactif existant
            var reactif = await _context.Reactifs.FirstOrDefaultAsync(r => r.IdReactif == id);

            if (reactif == null)
            {
                throw new Exception("Réactif non trouvé");
            }

            // Met à jour les propriétés du réactif
            reactif.Designation = reactifDto.Designation;
            reactif.IdTypeSortie = reactifDto.IdTypeSortie;
            reactif.IdUnite = reactifDto.IdUnite;

            // Sauvegarde les modifications
            await _context.SaveChangesAsync();

            // Convertit l'entité en DTO pour la réponse
            return ReactifMapper.ToDto(reactif);
        }

        // Supprime un réactif
        public async Task<bool> DeleteReactifAsync(int id)
        {
            // Récupère le réactif à supprimer
            var reactif = await _context.Reactifs.FirstOrDefaultAsync(r => r.IdReactif == id);

            if (reactif == null)
            {
                return false; // Réactif non trouvé
            }

            // Supprime le réactif
            _context.Reactifs.Remove(reactif);
            await _context.SaveChangesAsync();

            return true; // Suppression réussie
        }
    }
}
