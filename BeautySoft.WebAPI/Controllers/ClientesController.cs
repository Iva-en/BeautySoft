using BeautySoft.Infrastructure.Data;
using BeautySoft.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySoft.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly BeautySoftDbContext _db;
        public ClientesController(BeautySoftDbContext db) => _db = db;

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
        {
            var data = await _db.Clientes
                .OrderBy(c => c.Nombre)
                .Select(c => new ClienteDto
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.Nombre,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    Activo = c.Activo
                })
                .ToListAsync();
            return Ok(data);
        }
    }
}
