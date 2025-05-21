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
                query = query.Where(r => EF.Functions.Like(r.Designation, $"{searchTerm}%"));
            }

            var reactifs = await query
                .OrderBy(r => r.Designation)
                .ToListAsync();

            return reactifs.Select(ReactifMapper.ToDto);
        }

        public async Task<Dictionary<string, double>> GetStockEvolutionAsync(int reactifId, int year)
        {
            var stockEvolution = new Dictionary<string, double>();
            double stock = 0;

            // Récupérer les entrées
            var entrees = await _context.EntreeReactifs
                .Where(er => er.IdReactif == reactifId && er.DateEntree.Year <= year)
                .OrderBy(er => er.DateEntree)
                .ToListAsync();

            // Récupérer les sorties
            var sorties = await _context.SortieReactif
                .Where(sr => sr.IdReactif == reactifId && sr.DateSortie.Year <= year)
                .OrderBy(sr => sr.DateSortie)
                .ToListAsync();

            // Récupérer les rapports (inventaires)
            var rapports = await _context.ReportReactif
                .Where(rr => rr.IdReactif == reactifId && rr.DateReport.Year <= year)
                .OrderBy(rr => rr.DateReport)
                .ToListAsync();

            // Combiner toutes les transactions
            var transactions = entrees.Select(e => new { Date = e.DateEntree, Quantite = e.Quantite, Type = "Entree" })
                .Concat(sorties.Select(s => new { Date = s.DateSortie, Quantite = -s.Quantite, Type = "Sortie" }))
                .Concat(rapports.Select(r => new { Date = r.DateReport, Quantite = r.Quantite, Type = "Rapport" }))
                .OrderBy(t => t.Date)
                .ToList();

            // Calculer le stock initial (avant l'année demandée)
            var transactionsBeforeYear = transactions
                .Where(t => t.Date.Year < year)
                .ToList();

            foreach (var t in transactionsBeforeYear)
            {
                if (t.Type == "Rapport")
                {
                    stock = t.Quantite; // Réinitialiser le stock au dernier rapport
                }
                else
                {
                    stock += t.Quantite; // Ajouter ou soustraire la quantité
                }
            }

            // Traiter les transactions de l'année demandée
            var transactionsInYear = transactions
                .Where(t => t.Date.Year == year)
                .ToList();

            // Parcourir chaque mois de l'année
            for (int month = 1; month <= 12; month++)
            {
                var endOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                var transactionsThisMonth = transactionsInYear
                    .Where(t => t.Date.Year == year && t.Date.Month == month)
                    .ToList();

                // Mettre à jour le stock pour ce mois
                foreach (var t in transactionsThisMonth)
                {
                    if (t.Type == "Rapport")
                    {
                        stock = t.Quantite; // Réinitialiser le stock au rapport
                    }
                    else
                    {
                        stock += t.Quantite; // Ajouter ou soustraire la quantité
                    }
                }

                // Ajouter le stock au dictionnaire uniquement si le mois a des transactions ou si le stock est non nul
                if (transactionsThisMonth.Any() && stock >= 0)
                {
                    stockEvolution[$"{year}-{month:D2}"] = stock;
                }
            }

            return stockEvolution;
        }

        public async Task<double> GetCurrentStockAsync(int reactifId)
        {
            double stock = 0;

            // Récupérer toutes les entrées
            var entrees = await _context.EntreeReactifs
                .Where(er => er.IdReactif == reactifId)
                .OrderBy(er => er.DateEntree)
                .ToListAsync();

            // Récupérer toutes les sorties
            var sorties = await _context.SortieReactif
                .Where(sr => sr.IdReactif == reactifId)
                .OrderBy(sr => sr.DateSortie)
                .ToListAsync();

            // Récupérer tous les rapports (inventaires)
            var rapports = await _context.ReportReactif 
                .Where(rr => rr.IdReactif == reactifId)
                .OrderBy(rr => rr.DateReport)
                .ToListAsync();

            // Combiner toutes les transactions
            var transactions = entrees.Select(e => new { Date = e.DateEntree, Quantite = e.Quantite, Type = "Entree" })
                .Concat(sorties.Select(s => new { Date = s.DateSortie, Quantite = -s.Quantite, Type = "Sortie" }))
                .Concat(rapports.Select(r => new { Date = r.DateReport, Quantite = r.Quantite, Type = "Rapport" }))
                .OrderBy(t => t.Date)
                .ToList();

            // Calculer le stock actuel
            foreach (var t in transactions)
            {
                if (t.Type == "Rapport")
                {
                    stock = t.Quantite; // Réinitialiser le stock au dernier rapport
                }
                else
                {
                    stock += t.Quantite; // Ajouter ou soustraire la quantité
                }
            }

            return stock >= 0 ? stock : 0; // Retourner 0 si le stock est négatif
        }
    }
}