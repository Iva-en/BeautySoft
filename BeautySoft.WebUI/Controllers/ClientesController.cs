using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautySoft.Infrastructure.Data;
using BeautySoft.Domain.Entities;

namespace BeautySoft.WebUI.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BeautySoftDbContext _context;
        public ClientesController(BeautySoftDbContext context) => _context = context ?? throw new System.ArgumentNullException(nameof(context));

        public async Task<IActionResult> Index()
        {
            var data = await _context.Clientes.OrderBy(c => c.Nombre).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View(new Cliente());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente item)
        {
            if (!ModelState.IsValid) return View(item);
            _context.Clientes.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Clientes.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente item)
        {
            if (id != item.ClienteId) return NotFound();
            if (!ModelState.IsValid) return View(item);
            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Clientes.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int clienteId)
        {
            var item = await _context.Clientes.FindAsync(clienteId);
            if (item != null) { _context.Clientes.Remove(item); await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
