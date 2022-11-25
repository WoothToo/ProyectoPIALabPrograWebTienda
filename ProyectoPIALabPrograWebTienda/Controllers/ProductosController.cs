using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPIALabPrograWebTienda.Models.dbModels;
using ProyectoPIALabPrograWebTienda.Models.DTO;

namespace ProyectoPIALabPrograWebTienda.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductosController : Controller
    {
        private readonly ProyectoPIALabPrograWebTiendaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductosController(ProyectoPIALabPrograWebTiendaContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var proyectoPIALabPrograWebTiendaContext = _context.Productos.Include(p => p.IdcategoriaNavigation);
            return View(await proyectoPIALabPrograWebTiendaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdcategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["Idcategoria"] = new SelectList(_context.Categoria, "Idcategoria", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoCreateDTO producto)
        {
            if (ModelState.IsValid)
            {
                string? fileName = await GuardarFotografiaProductoAsync(producto.ImagenArchivo);
                Producto p = new Producto
                {
                    Nombre = producto.Nombre,
                    Idcategoria = producto.Idcategoria,
                    PrecioUnitario = producto.PrecioUnitario,
                    Imagen = fileName
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcategoria"] = new SelectList(_context.Categoria, "Idcategoria", "Nombre", producto.Idcategoria);
            return View(producto);
        }

        public async Task<string?> ReemplazarFotografiaAsync(IFormFile? file, string? fileToReplace)
        {
            if (file != null)
            {
                string? newFileName = await GuardarFotografiaProductoAsync(file);
                if (newFileName != null)
                {
                    BorrarFotografiaProducto(fileToReplace);
                    return newFileName;
                }
            }
            return null;
        }

        public async Task<string?> GuardarFotografiaProductoAsync(IFormFile? file)
        {
            if (file != null)
            {
                try
                {
                    string folder = "Fotos/Productos/";
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder + fileName);

                    using (FileStream stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        //Copies data from entity.file to stream
                        await file.CopyToAsync(stream);
                    }
                    return fileName;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public bool BorrarFotografiaProducto(string? fileName)
        {
            if (fileName != null)
            {
                try
                {
                    string folder = "Fotos/Productos/" + fileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    FileInfo fileInfo = new FileInfo(serverFolder);
                    fileInfo.Delete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            ProductoUpdateDTO productoUpdate = new ProductoUpdateDTO
            {
                Idproducto = producto.Idproducto,
                Nombre = producto.Nombre,
                Idcategoria = producto.Idcategoria,
                PrecioUnitario = producto.PrecioUnitario,
                Imagen = producto.Imagen
            };
            ViewData["Idcategoria"] = new SelectList(_context.Categoria, "Idcategoria", "Nombre", producto.Idcategoria);
            return View(productoUpdate);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoUpdateDTO producto)
        {
            if (id != producto.Idproducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string? fileName = await ReemplazarFotografiaAsync(producto.ImagenArchivo, producto.Imagen);
                    Producto p = new Producto
                    {
                        Idproducto = producto.Idproducto,
                        Nombre = producto.Nombre,
                        Idcategoria = producto.Idcategoria,
                        PrecioUnitario = producto.PrecioUnitario,
                        Imagen = fileName == null ? producto.Imagen : fileName
                    };
                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Idproducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcategoria"] = new SelectList(_context.Categoria, "Idcategoria", "Nombre", producto.Idcategoria);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdcategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'ProyectoPIALabPrograWebTiendaContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return _context.Productos.Any(e => e.Idproducto == id);
        }
    }
}
