using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VentaAutos.Models


{
    public class Auto
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }

        [Display(Name = "Año")]
        public string? Anio { get; set; }
        
        [Precision(18, 2)]
        public decimal Precio { get; set; }

        [Display(Name = "Kilometro")]
        public int? Km { get; set; }

        public string? Color { get; set; }

        public string? Detalles { get; set; }

        public string? Motor { get; set; }

        public string? Transmision { get; set; }

        public string? Capacidad_de_carga { get;set; }

        public string? Seguridad { get;set; }

        public string? Conbustible { get; set; }

        public virtual ICollection<Imagen> Imagenes { get; set; } = new List<Imagen>();

    }

    public class Imagen
    {

        public int Id { get; set; }
        public string? ImagenUrl { get; set; }

        public int IdAuto { get; set; }

        public Auto Auto { get; set; } = null!;

    }

}
