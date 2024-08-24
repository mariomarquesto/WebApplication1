using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Datos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public InicioController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // Acción GET para mostrar la lista de contactos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactos = await _contexto.Contactos.ToListAsync();
            return View(contactos);
        }

        // Acción GET para mostrar el formulario de creación de contacto
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // Acción POST para manejar la creación de un nuevo contacto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Contactos.Add(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacto); // Reenvía el modelo con los errores al formulario
        }


        //Accion para Editar un Contacto

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contactos.Find(id);
            if(contacto ==null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacto); // Reenvía el modelo con los errores al formulario
        }


        // Acción GET para mostrar la página de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción GET para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

