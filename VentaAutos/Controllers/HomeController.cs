using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using VentaAutos.Models;
using VentaAutos.ViewModels;


namespace VentaAutos.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ActiveController"] = "Home";
            ViewData["ActiveAction"] = "Index";
            return View();
        }

        public IActionResult Nosotros()
        {
            ViewData["ActiveController"] = "Home";
            ViewData["ActiveAction"] = "Nosotros";
            return View();
        }

        [HttpGet("Home/Autos")]
        public async Task<IActionResult> Autos(string orden, string marca, string modelo, int? precio_max, int? precio_min, string anio, string color, string kmSeleccionado,int? kmMin,int? kmMax)
        {

            try
            {
                ViewData["ActiveController"] = "Home";
                ViewData["ActiveAction"] = "Autos";

                var establecer_precio_max = await _context.Autos.Where(a => a.Precio > 0).MaxAsync(a => (decimal?)a.Precio) ?? 20000000M;

                var establecer_km_max = await _context.Autos.Where(a => a.Km.HasValue && a.Km > 0).MaxAsync(a => (int?)a.Km);

                decimal establecer_precio_max_minimo = establecer_precio_max;

                if (precio_max.HasValue)
                {

                    establecer_precio_max_minimo = (int)precio_max;

                }

                var autosQuery = _context.Autos.AsQueryable();

                if (!string.IsNullOrEmpty(marca))
                    autosQuery = autosQuery.Where(a => a.Marca != null && a.Marca.ToLower().Equals(marca.ToLower()));

                if (!string.IsNullOrEmpty(modelo))
                    autosQuery = autosQuery.Where(a => a.Modelo != null && a.Modelo.ToLower().Equals(modelo.ToLower()));

                if (!string.IsNullOrEmpty(anio))
                    autosQuery = autosQuery.Where(a => a.Anio != null && a.Anio.Equals(anio.ToLower()));

                if (!string.IsNullOrEmpty(color))
                    autosQuery = autosQuery.Where(a => a.Color != null && a.Color.ToLower().Equals(color.ToLower()));

                if (kmMin.HasValue)
                    autosQuery = autosQuery.Where(a => a.Km.HasValue && a.Km.GetValueOrDefault() >= kmMin);

                if (kmMax.HasValue)
                    autosQuery = autosQuery.Where(a => a.Km.HasValue && a.Km.GetValueOrDefault() <= kmMax);

                if (precio_min.HasValue && precio_min > 0)
                    autosQuery = autosQuery.Where(a => a.Precio >= precio_min);

                if (precio_max.HasValue && precio_max > 0)
                    autosQuery = autosQuery.Where(a => a.Precio <= precio_max);

                switch (orden?.ToUpper())
                {
                    case "MENOR":
                        autosQuery = autosQuery.OrderBy(a => a.Precio);
                        break;
                    case "MAYOR":
                        autosQuery = autosQuery.OrderByDescending(a => a.Precio);
                        break;
                    case "RELEVANCIA":
                    default:
                        autosQuery = autosQuery.OrderBy(a => a.Id);
                        break;
                }

                var autos = autosQuery.Include(a => a.Imagenes).AsQueryable();

                var viewModels = new AutosViewModels
                {
                    Autos = autos,
                    Listado_de_marcas = autos.Where(a => a.Marca != null).GroupBy(a => a.Marca).Select(g => g.Key).OrderBy(m => m).ToList()!,
                    Listado_de_modelos = autos.Where(a => a.Marca == marca && a.Modelo != null).GroupBy(a => a.Modelo).Select(g => g.Key).OrderBy(m => m).ToList()!,
                    Listado_de_anios = autos.Where(a => a.Anio != null).GroupBy(a => a.Anio).Select(g => g.Key).OrderBy(a => a).ToList()!,
                    Listado_de_colores = autos.Where(a => a.Color != null).GroupBy(a => a.Color).Select(g => g.Key).OrderBy(a => a).ToList()!,
                    AutosCon0km = autos.Where(a => a.Km == 0).Count().ToString(),
                    AutosCon0A60000 = autos.Where(a => a.Km >= 0 && a.Km <= 60000).Count().ToString(),
                    AutosCon60000A80000 = autos.Where(a => a.Km > 60000 && a.Km <= 80000).Count().ToString(),
                    AutosCon80000A100000 = autos.Where(a => a.Km > 80000 && a.Km <= 100000).Count().ToString(),
                    AutosCon100000oMas = autos.Where(a => a.Km > 100000).Count().ToString(),

                    MarcaSeleccionada = marca,
                    ModeloSeleccionado = modelo,
                    AnioSeleccionado = anio,
                    KmSeleccionado = kmSeleccionado,
                    ColorSeleccionado = color,
                    PrecioMax = precio_max,
                    PrecioMin = precio_min,
                    PrecioMaxDb = establecer_precio_max,
                    KmMaxDb = establecer_km_max,
                    PrecioMaxMin = establecer_precio_max_minimo

                };

                return View(viewModels);
            }
            catch (Exception ex)
            {
                
                return Content("Error: " + ex.Message + "\n" + ex.StackTrace);
            }

            

        }

    }
}

