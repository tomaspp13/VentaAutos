using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace VentaAutos.Models 
{
    using Microsoft.EntityFrameworkCore;
    using VentaAutos.Models;

    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public DbSet<Auto> Autos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>(entity =>
            {
                entity.Property(a => a.Marca).HasColumnType("text");
                entity.Property(a => a.Modelo).HasColumnType("text");
                entity.Property(a => a.Anio).HasColumnType("text");
                entity.Property(a => a.Color).HasColumnType("text");
                entity.Property(a => a.Detalles).HasColumnType("text");
                entity.Property(a => a.Motor).HasColumnType("text");
                entity.Property(a => a.Transmision).HasColumnType("text");
                entity.Property(a => a.Capacidad_de_carga).HasColumnType("text");
                entity.Property(a => a.Seguridad).HasColumnType("text");
                entity.Property(a => a.Conbustible).HasColumnType("text");
                entity.Property(a => a.Precio)
                      .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Imagen>()
                .HasOne(i => i.Auto)
                .WithMany(a => a.Imagenes)
                .HasForeignKey(i => i.IdAuto)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}