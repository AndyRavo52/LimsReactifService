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
    public class TypeSortieService : ITypeSortieService
    {
        private readonly ReactifServiceContext _context;

        // Constructeur : injection du contexte de base de données
        public TypeSortieService(ReactifServiceContext context)
        {
            _context = context;
        }

        // Compte le nombre total de types de sortie
        public async Task<int> CountTypesSortieAsync()
        {
            return await _context.TypesSortie.CountAsync();
        }

        // Récupère une liste paginée de types de sortie
        public async Task<IEnumerable<TypeSortieDto>> GetTypesSortieAsync(int pageIndex, int pageSize)
        {
            var typesSortie = await _context.TypesSortie
                .OrderBy(ts => ts.Designation)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Convertit les entités en DTOs
            return typesSortie.Select(ts => new TypeSortieDto
            {
                IdTypeSortie = ts.IdTypeSortie,
                Designation = ts.Designation
            });
        }

        // Récupère un type de sortie par son ID
        public async Task<TypeSortieDto> GetTypeSortieByIdAsync(int id)
        {
            var typeSortie = await _context.TypesSortie.FindAsync(id);

            if (typeSortie == null)
            {
                throw new Exception("Type de sortie non trouvé");
            }

            // Convertit l'entité en DTO
            return new TypeSortieDto
            {
                IdTypeSortie = typeSortie.IdTypeSortie,
                Designation = typeSortie.Designation
            };
        }

        // Crée un nouveau type de sortie
        public async Task<TypeSortieDto> CreateTypeSortieAsync(TypeSortieDto typeSortieDto)
        {
            // Convertit le DTO en entité
            var typeSortie = new TypeSortie
            {
                Designation = typeSortieDto.Designation
            };

            // Ajoute le type de sortie à la base de données
            _context.TypesSortie.Add(typeSortie);
            await _context.SaveChangesAsync();

            // Affecte l'ID généré au DTO
            typeSortieDto.IdTypeSortie = typeSortie.IdTypeSortie;

            // Retourne le DTO mis à jour
            return typeSortieDto;
        }

        // Met à jour un type de sortie existant
        public async Task<TypeSortieDto> UpdateTypeSortieAsync(int id, TypeSortieDto typeSortieDto)
        {
            // Récupère l'entité existante
            var typeSortie = await _context.TypesSortie.FindAsync(id);

            if (typeSortie == null)
            {
                throw new Exception("Type de sortie non trouvé");
            }

            // Met à jour les propriétés
            typeSortie.Designation = typeSortieDto.Designation;

            await _context.SaveChangesAsync();

            return typeSortieDto;
        }

        // Supprime un type de sortie
        public async Task<bool> DeleteTypeSortieAsync(int id)
        {
            var typeSortie = await _context.TypesSortie.FindAsync(id);

            if (typeSortie == null)
            {
                return false; // Type de sortie non trouvé
            }

            _context.TypesSortie.Remove(typeSortie);
            await _context.SaveChangesAsync();

            return true; // Suppression réussie
        }
    }
}
