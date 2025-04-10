using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace VentaAutos.Models  // Asegúrate de usar el nombre correcto de tu proyecto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Auto> Autos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imagen>()
    .HasOne(i => i.Auto)
    .WithMany(a => a.Imagenes)
    .HasForeignKey(i => i.IdAuto)
    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Auto>(entity =>
            {
                entity.Property(a => a.Precio)
                      .HasPrecision(18, 2); 
            });

        }
    }
}