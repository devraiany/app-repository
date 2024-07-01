using App.Repository.Domain.Entities.Favoritos;
using App.Repository.Domain.Entities.Repositorios;
using App.Repository.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Data.Databases
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Favoritos> Favoritos { get; set; }
        public DbSet<Repositorios> Repositorios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
