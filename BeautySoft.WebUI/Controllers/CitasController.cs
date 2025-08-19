using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySoft.Infrastructure.Data;
using BeautySoft.Domain.Entities;

namespace BeautySoft.WebUI.Controllers
{
    public class CitasController : Controller
    {
        private readonly BeautySoftDbContext _context;
        public CitasController(BeautySoftDbContext context) => _context = context ?? throw new System.ArgumentNullException(nameof(context));

        public async Task<IActionResult> Index()
        {
            var data = await _context.Citas
                .OrderBy(c => c.FechaHora)
                .ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Citas
                .Include(c => c.Cliente).Include(c => c.Estilista).Include(c => c.Servicio)
                .FirstOrDefaultAsync(c => c.CitaId == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCombosAsync();
            return View(new Cita { FechaHora = System.DateTime.Now.AddHours(1), Estado = EstadoCita.Programada });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cita item)
        {
            if (!ModelState.IsValid)
            {
                await LoadCombosAsync();
                return View(item);
            }
            _context.Citas.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Citas.FindAsync(id);
            if (item == null) return NotFound();
            await LoadCombosAsync();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cita item)
        {
            if (id != item.CitaId) return NotFound();
            if (!ModelState.IsValid)
            {
                await LoadCombosAsync();
                return View(item);
            }
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Citas
                .Include(c => c.Cliente).Include(c => c.Estilista).Include(c => c.Servicio)
                .FirstOrDefaultAsync(c => c.CitaId == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int citaId)
        {
            var item = await _context.Citas.FindAsync(citaId);
            if (item != null) { _context.Citas.Remove(item); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCombosAsync()
        {
            var clientes = await _context.Clientes.Where(x => x.Activo).OrderBy(x => x.Nombre).ToListAsync();
            var estilistas = await _context.Estilistas.Where(x => x.Activo).OrderBy(x => x.Nombre).ToListAsync();
            var servicios = await _context.Servicios.Where(x => x.Activo).OrderBy(x => x.Nombre).ToListAsync();

            ViewBag.Clientes = new SelectList(clientes, "ClienteId", "Nombre");
            ViewBag.Estilistas = new SelectList(estilistas, "EstilistaId", "Nombre");
            ViewBag.Servicios = new SelectList(servicios, "ServicioId", "Nombre");
            ViewBag.Estados = new SelectList(new[] { "Programada", "Completada", "Cancelada", "NoShow" });
        }
    }
}
