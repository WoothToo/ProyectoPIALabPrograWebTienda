using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPIALabPrograWebTienda.Models.dbModels;
using ProyectoPIALabPrograWebTienda.Models.DTO;

namespace ProyectoPIALabPrograWebTienda.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ProyectoPIALabPrograWebTiendaContext _context;

        public CarritoController(ProyectoPIALabPrograWebTiendaContext context)
        {
            _context = context;
        }

        // GET: Carrito
        public async Task<IActionResult> Index()
        {
            //var proyectoPIALabPrograWebTiendaContext = _context.DetallesVenta.Include(d => d.IdproductoNavigation).Include(d => d.IdventaNavigation);
            var proyectoPIALabPrograWebTiendaContext = _context.DetallesVenta.Include(d => d.IdproductoNavigation).Include(d => d.IdventaNavigation);
            return View(await proyectoPIALabPrograWebTiendaContext.ToListAsync());
        }

        // GET: Carrito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallesVenta == null)
            {
                return NotFound();
            }

            var detallesVentum = await _context.DetallesVenta
                .Include(d => d.IdproductoNavigation)
                .Include(d => d.IdventaNavigation)
                .FirstOrDefaultAsync(m => m.Idventa == id);
            if (detallesVentum == null)
            {
                return NotFound();
            }

            return View(detallesVentum);
        }

        // GET: Carrito/Create
        public IActionResult Create()
        {
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto");
            ViewData["Idventa"] = new SelectList(_context.Venta, "Idventa", "Idventa");
            return View();
        }

        // POST: Carrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Idventa,Idproducto,Precio,Cantidad")] DetallesVentum detallesVentum)
        public async Task<IActionResult> Create(int idProducto)
        {
            if (ModelState.IsValid)
            {
                Carrito carrito = new Carrito();
                carrito.Idproducto = idProducto;
               // carrito.Idusuario = 

               // _context.Add(carrito);
               // await _context.SaveChangesAsync();
               // return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Carrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetallesVenta == null)
            {
                return NotFound();
            }

            var detallesVentum = await _context.DetallesVenta.FindAsync(id);
            if (detallesVentum == null)
            {
                return NotFound();
            }
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto", detallesVentum.Idproducto);
            ViewData["Idventa"] = new SelectList(_context.Venta, "Idventa", "Idventa", detallesVentum.Idventa);
            return View(detallesVentum);
        }

        // POST: Carrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idventa,Idproducto,Precio,Cantidad")] DetallesVentum detallesVentum)
        {
            if (id != detallesVentum.Idventa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallesVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesVentumExists(detallesVentum.Idventa))
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
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto", detallesVentum.Idproducto);
            ViewData["Idventa"] = new SelectList(_context.Venta, "Idventa", "Idventa", detallesVentum.Idventa);
            return View(detallesVentum);
        }

        // GET: Carrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetallesVenta == null)
            {
                return NotFound();
            }

            var detallesVentum = await _context.DetallesVenta
                .Include(d => d.IdproductoNavigation)
                .Include(d => d.IdventaNavigation)
                .FirstOrDefaultAsync(m => m.Idventa == id);
            if (detallesVentum == null)
            {
                return NotFound();
            }

            return View(detallesVentum);
        }

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetallesVenta == null)
            {
                return Problem("Entity set 'ProyectoPIALabPrograWebTiendaContext.DetallesVenta'  is null.");
            }
            var detallesVentum = await _context.DetallesVenta.FindAsync(id);
            if (detallesVentum != null)
            {
                _context.DetallesVenta.Remove(detallesVentum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesVentumExists(int id)
        {
          return _context.DetallesVenta.Any(e => e.Idventa == id);
        }
    }
}
