using Microsoft.AspNetCore.Mvc;
using ProyectoPIALabPrograWebTienda.Models;
using ProyectoPIALabPrograWebTienda.Models.dbModels;
using System.Diagnostics;

namespace ProyectoPIALabPrograWebTienda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProyectoPIALabPrograWebTiendaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ProyectoPIALabPrograWebTiendaContext context, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("[controller]/ProductosTienda/{Idcategoria?}")]
        public IActionResult ProductosTienda(int? Idcategoria = null)
        {
            List<Producto> ListaProducto = null;
            if (Idcategoria == null)
            {
                ListaProducto = _context.Productos.ToList();
            }
            else
            {
                ListaProducto = _context.Productos.Where(x=>x.Idcategoria == Idcategoria).ToList();
            }
            
            return View(ListaProducto);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}