using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

//1.- AÑADIR LA AUTORIZACION
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    //2.- AÑADIR LA AUTORIZACION
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly ApplicationDbContext _contexto;
        public HomeController(ApplicationDbContext contexto, ILogger<HomeController> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        [Authorize(Roles = "Administrador,Vendedor,Empleado")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Productos> listaProductos = _contexto.Productos.ToList();
            return View(listaProductos);
        }

        [Authorize(Roles = "Administrador,Vendedor")]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Productos producto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Productos.Add(producto);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Administrador,Vendedor")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var producto = _contexto.Productos.FirstOrDefault(c => c.Productos_Id == id);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Productos producto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Productos.Update(producto);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            var producto = _contexto.Productos.FirstOrDefault(c => c.Productos_Id == id);
            _contexto.Productos.Remove(producto);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
