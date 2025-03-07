using System.ComponentModel;
using LimsReactifService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsReactifService.Data
{
    public class ReactifServiceContext : DbContext
    {
        public ReactifServiceContext(DbContextOptions<ReactifServiceContext> options)
            : base(options)
        {
        }

        public DbSet<TypeSortie> TypesSortie { get; set; }
        public DbSet<Unite> Unites { get; set; }
        public DbSet<Reactif> Reactifs { get; set; }
    }
}
