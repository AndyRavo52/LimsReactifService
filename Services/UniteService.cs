using LimsReactifService.Data;
using LimsReactifService.Dtos;
using LimsReactifService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimsReactifService.Services
{
    public class UniteService : IUniteService
    {
        private readonly ReactifServiceContext _context;

        // Constructeur : injection du contexte de base de données
        public UniteService(ReactifServiceContext context)
        {
            _context = context;
        }

        // Compte le nombre total d'unités
        public async Task<int> CountUnitesAsync()
        {
            return await _context.Unites.CountAsync();
        }

        // Récupère une liste paginée d'unités
        public async Task<IEnumerable<UniteDto>> GetUnitesAsync(int pageIndex, int pageSize)
        {
            var unites = await _context.Unites
                .OrderBy(u => u.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Convertit les entités en DTOs
            return unites.Select(u => new UniteDto
            {
                IdUnite = u.IdUnite,
                Designation = u.Designation
            });
        }

        // Récupère une unité par son ID
        public async Task<UniteDto> GetUniteByIdAsync(int id)
        {
            var unite = await _context.Unites.FindAsync(id);

            if (unite == null)
            {
                throw new Exception("Unité non trouvée");
            }

            // Convertit l'entité en DTO
            return new UniteDto
            {
                IdUnite = unite.IdUnite,
                Designation = unite.Designation
            };
        }

        // Crée une nouvelle unité
        public async Task<UniteDto> CreateUniteAsync(UniteDto uniteDto)
        {
            // Convertit le DTO en entité
            var unite = new Unite
            {
                Designation = uniteDto.Designation
            };

            // Ajoute l'unité à la base de données
            _context.Unites.Add(unite);
            await _context.SaveChangesAsync();

            // Affecte l'ID généré au DTO
            uniteDto.IdUnite = unite.IdUnite;

            // Retourne le DTO mis à jour
            return uniteDto;
        }

        // Met à jour une unité existante
        public async Task<UniteDto> UpdateUniteAsync(int id, UniteDto uniteDto)
        {
            // Récupère l'entité existante
            var unite = await _context.Unites.FindAsync(id);

            if (unite == null)
            {
                throw new Exception("Unité non trouvée");
            }

            // Met à jour les propriétés
            unite.Designation = uniteDto.Designation;

            await _context.SaveChangesAsync();

            return uniteDto;
        }

        // Supprime une unité
        public async Task<bool> DeleteUniteAsync(int id)
        {
            var unite = await _context.Unites.FindAsync(id);

            if (unite == null)
            {
                return false; // Unité non trouvée
            }

            _context.Unites.Remove(unite);
            await _context.SaveChangesAsync();

            return true; // Suppression réussie
        }
    }
}
