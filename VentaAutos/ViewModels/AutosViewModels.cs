using VentaAutos.Models;
namespace VentaAutos.ViewModels
{
    public class AutosViewModels
    {
        public IEnumerable<Auto>? Autos { get; set; }

        public List<string>? Listado_de_marcas { get; set; }
        public List<string>? Listado_de_modelos { get; set; }
        public List<string>? Listado_de_colores { get; set; }
        public List<string>? Listado_de_anios { get; set; }

        public string? MarcaSeleccionada { get; set; }
        public string? ModeloSeleccionado { get; set; }
        public string? AnioSeleccionado { get; set; }
        public string? KmSeleccionado { get; set; }
        public int? KmMax { get; set; }
        public int? KmMin { get; set; }
        public string? AutosCon0km { get; set; }
        public string? AutosCon0A60000 { get; set; }
        public string? AutosCon60000A80000 { get; set; }
        public string? AutosCon80000A100000 { get; set; }
        public string? AutosCon100000oMas { get; set; }
        public string? ColorSeleccionado { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public decimal? PrecioMaxDb { get; set; }
        public decimal? PrecioMaxMin { get; set; }
        public int? KmMaxDb { get; set; }

    }
}
