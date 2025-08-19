using BeautySoft.Infrastructure.Data;
using BeautySoft.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySoft.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstilistasController : ControllerBase
    {
        private readonly BeautySoftDbContext _db;
        public EstilistasController(BeautySoftDbContext db) => _db = db;

        // GET: api/estilistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstilistaDto>>> GetAll()
        {
            var data = await _db.Estilistas
                .OrderBy(e => e.Nombre)
                .Select(e => new EstilistaDto
                {
                    EstilistaId = e.EstilistaId,
                    Nombre = e.Nombre,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    PorcentajeComision = e.PorcentajeComision,
                    Activo = e.Activo
                })
                .ToListAsync();
            return Ok(data);
        }
    }
}
