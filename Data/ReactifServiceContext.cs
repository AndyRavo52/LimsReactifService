using System.ComponentModel;
using LimsReactifService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsReactifService.Data
{
    public class ReactifServiceContext : DbContext
    {
        public ReactifServiceContext(DbContextOptions<ReactifServiceContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResteStock>()
                .HasNoKey();
        }

        public DbSet<TypeSortie> TypesSortie { get; set; }
        public DbSet<Unite> Unites { get; set; }
        public DbSet<Reactif> Reactifs { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<EntreeReactif> EntreeReactifs { get; set; }
        public DbSet<ObjetSortieReactif> ObjetSortieReactif { get; set; }
        public DbSet<SortieReactif> SortieReactif { get; set; }
        public DbSet<Departement> Departement { get; set; }
        public DbSet<ReportReactif> ReportReactif { get; set; }
        public DbSet<ResteStock> ResteStocks { get; set; }
    }
}
