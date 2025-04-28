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
    public class ObjetSortieReactifService : IObjetSortieReactifService
    {
        private readonly ReactifServiceContext _context;

        // Constructeur : injection du contexte de base de données
        public ObjetSortieReactifService(ReactifServiceContext context)
        {
            _context = context;
        }

        // Compte le nombre total d'objets de sortie réactif
        public async Task<int> CountObjetsSortieReactifAsync()
        {
            return await _context.ObjetSortieReactif.CountAsync();
        }

        // Récupère une liste paginée d'objets de sortie réactif
        public async Task<IEnumerable<ObjetSortieReactifDto>> GetObjetsSortieReactifAsync(int pageIndex, int pageSize)
        {
            var objetsSortieReactif = await _context.ObjetSortieReactif
                .OrderBy(osr => osr.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Convertit les entités en DTOs
            return objetsSortieReactif.Select(osr => new ObjetSortieReactifDto
            {
                IdObjetSortieReactif = osr.IdObjetSortieReactif,
                Designation = osr.Designation
            });
        }

        // Récupère un objet de sortie réactif par son ID
        public async Task<ObjetSortieReactifDto> GetObjetSortieReactifByIdAsync(int id)
        {
            var objetSortieReactif = await _context.ObjetSortieReactif.FindAsync(id);

            if (objetSortieReactif == null)
            {
                throw new Exception("Objet de sortie réactif non trouvé");
            }

            // Convertit l'entité en DTO
            return new ObjetSortieReactifDto
            {
                IdObjetSortieReactif = objetSortieReactif.IdObjetSortieReactif,
                Designation = objetSortieReactif.Designation
            };
        }

        // Crée un nouvel objet de sortie réactif
        public async Task<ObjetSortieReactifDto> CreateObjetSortieReactifAsync(ObjetSortieReactifDto objetSortieReactifDto)
        {
            // Convertit le DTO en entité
            var objetSortieReactif = new ObjetSortieReactif
            {
                Designation = objetSortieReactifDto.Designation
            };

            // Ajoute l'objet de sortie réactif à la base de données
            _context.ObjetSortieReactif.Add(objetSortieReactif);
            await _context.SaveChangesAsync();

            // Affecte l'ID généré au DTO
            objetSortieReactifDto.IdObjetSortieReactif = objetSortieReactif.IdObjetSortieReactif;

            // Retourne le DTO mis à jour
            return objetSortieReactifDto;
        }

        // Met à jour un objet de sortie réactif existant
        public async Task<ObjetSortieReactifDto> UpdateObjetSortieReactifAsync(int id, ObjetSortieReactifDto objetSortieReactifDto)
        {
            // Récupère l'entité existante
            var objetSortieReactif = await _context.ObjetSortieReactif.FindAsync(id);

            if (objetSortieReactif == null)
            {
                throw new Exception("Objet de sortie réactif non trouvé");
            }

            // Met à jour les propriétés
            objetSortieReactif.Designation = objetSortieReactifDto.Designation;

            await _context.SaveChangesAsync();

            return objetSortieReactifDto;
        }

        // Supprime un objet de sortie réactif
        public async Task<bool> DeleteObjetSortieReactifAsync(int id)
        {
            var objetSortieReactif = await _context.ObjetSortieReactif.FindAsync(id);

            if (objetSortieReactif == null)
            {
                return false; // Objet de sortie réactif non trouvé
            }

            _context.ObjetSortieReactif.Remove(objetSortieReactif);
            await _context.SaveChangesAsync();

            return true; // Suppression réussie
        }
    }
}