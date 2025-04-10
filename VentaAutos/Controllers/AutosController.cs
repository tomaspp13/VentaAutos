using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VentaAutos.Models;

namespace VentaAutos.Controllers
{
    public class AutosController : Controller
    {
        private readonly AppDbContext _context;

        public AutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Autos
        public async Task<IActionResult> Index()
        {
            var autos = await _context.Autos.Include(a => a.Imagenes).ToListAsync();

            return View(autos);
        }


        // GET: Autos/Details/5
        public async Task<IActionResult> Details(int? id, bool desdeAdmin = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos
                .Include(a => a.Imagenes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (auto == null)
            {
                return NotFound();
            }

            ViewBag.DesdeAdmin = desdeAdmin;

            return View(auto);
        }


        // GET: Autos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Auto auto, List<IFormFile> imagenes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auto);
                await _context.SaveChangesAsync();

                if (imagenes != null && imagenes.Any())
                {
                    var listaImagenes = new List<Imagen>();

                    foreach (var imagen in imagenes)
                    {
                        if (imagen.Length > 0)
                        {
                            
                            var fileName = Path.GetFileName(imagen.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imagen.CopyToAsync(stream);
                            }

                            listaImagenes.Add(new Imagen
                            {
                                ImagenUrl = "/images/" + fileName,
                                IdAuto = auto.Id
                            });
                        }
                    }

                    _context.Imagenes.AddRange(listaImagenes);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(auto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.Include(a => a.Imagenes).FirstOrDefaultAsync(m => m.Id == id); 

            if (auto == null)
            {
                return NotFound();
            }
            return View(auto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,string Marca,string Modelo,string Anio,decimal Precio,int Km,string Color,
         string Motor,string Transmision, string Capacidad_de_carga,string Seguridad, string Conbustible, string Detalles,IFormFile[] imagenes,
         int[] imagenesToDelete)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var autoDb = await _context.Autos.Include(a => a.Imagenes).FirstOrDefaultAsync(a => a.Id == id);
            if (autoDb == null)
            {
                return NotFound();
            }

            autoDb.Marca = Marca;
            autoDb.Modelo = Modelo;
            autoDb.Anio = Anio;
            autoDb.Precio = Precio;
            autoDb.Km = Km;
            autoDb.Color = Color;
            autoDb.Motor = Motor;
            autoDb.Transmision = Transmision;
            autoDb.Capacidad_de_carga = Capacidad_de_carga;
            autoDb.Seguridad = Seguridad;
            autoDb.Conbustible = Conbustible;
            autoDb.Detalles = Detalles;

            if (imagenesToDelete != null && imagenesToDelete.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                foreach (var imagenId in imagenesToDelete)
                {
                    var imagen = autoDb.Imagenes.FirstOrDefault(i => i.Id == imagenId);
                    if (imagen != null)
                    {
                        var filePath = Path.Combine(uploadsFolder, Path.GetFileName(imagen.ImagenUrl ?? string.Empty));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        _context.Imagenes.Remove(imagen);
                    }
                }
            }
           
            if (imagenes != null && imagenes.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                foreach (var imagen in imagenes)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                 
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    var imagenEntity = new Imagen
                    {
                        ImagenUrl = "/images/" + fileName,
                        IdAuto = autoDb.Id
                    };
                    _context.Imagenes.Add(imagenEntity);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteAutoConId(autoDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ExisteAutoConId(int id)
        {
            return _context.Autos.Any(e => e.Id == id); 
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.Include(a => a.Imagenes).FirstOrDefaultAsync(m => m.Id == id);

            if (auto == null)
            {
                return NotFound();
            }

            if (auto.Imagenes == null)
            {
                auto.Imagenes = new List<Imagen>();
            }

            return View(auto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var auto = await _context.Autos.Include(a => a.Imagenes).FirstOrDefaultAsync(a => a.Id == id);

            if (auto != null)
            {

                foreach (var imagen in auto.Imagenes)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagen.ImagenUrl?.TrimStart('/') ?? string.Empty);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    _context.Imagenes.Remove(imagen);
                }

                _context.Autos.Remove(auto);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}