using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautySoft.Infrastructure.Data;
using BeautySoft.Domain.Entities;

namespace BeautySoft.WebUI.Controllers
{
    public class EstilistasController : Controller
    {
        private readonly BeautySoftDbContext _context;
        public EstilistasController(BeautySoftDbContext context) => _context = context ?? throw new System.ArgumentNullException(nameof(context));

        public async Task<IActionResult> Index()
        {
            var data = await _context.Estilistas.OrderBy(e => e.Nombre).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Estilistas.FirstOrDefaultAsync(e => e.EstilistaId == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View(new Estilista());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estilista item)
        {
            if (!ModelState.IsValid) return View(item);
            _context.Estilistas.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Estilistas.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estilista item)
        {
            if (id != item.EstilistaId) return NotFound();
            if (!ModelState.IsValid) return View(item);
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Estilistas.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int estilistaId)
        {
            var item = await _context.Estilistas.FindAsync(estilistaId);
            if (item != null) { _context.Estilistas.Remove(item); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
