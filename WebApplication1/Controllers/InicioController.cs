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

        // Acción GET para mostrar el formulario de edición de contacto
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // Acción POST para manejar la edición de un contacto
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

        // Acción GET para mostrar los detalles de un contacto
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // Acción GET para mostrar el formulario de confirmación de eliminación
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacto = _contexto.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // Acción POST para manejar la eliminación de un contacto
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var contacto = await _contexto.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            _contexto.Contactos.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
