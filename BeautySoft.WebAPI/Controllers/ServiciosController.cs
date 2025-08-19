using BeautySoft.Domain.Entities;
using BeautySoft.Infrastructure.Data;
using BeautySoft.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySoft.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly BeautySoftDbContext _db;

        public ServiciosController(BeautySoftDbContext db) => _db = db;

        // GET: api/servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioDto>>> Get()
        {
            var data = await _db.Servicios
                .OrderBy(s => s.Nombre)
                .Select(s => new ServicioDto
                {
                    ServicioId = s.ServicioId,
                    Nombre = s.Nombre,
                    DuracionMinutos = s.DuracionMinutos,
                    Precio = s.Precio,
                    Activo = s.Activo
                })
                .ToListAsync();

            return Ok(data);
        }

        // GET: api/servicios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicioDto>> Get(int id)
        {
            var s = await _db.Servicios.FindAsync(id);
            if (s == null) return NotFound();

            return Ok(new ServicioDto
            {
                ServicioId = s.ServicioId,
                Nombre = s.Nombre,
                DuracionMinutos = s.DuracionMinutos,
                Precio = s.Precio,
                Activo = s.Activo
            });
        }

        // POST: api/servicios
        [HttpPost]
        public async Task<ActionResult<ServicioDto>> Post(ServicioDto dto)
        {
            var s = new Servicio
            {
                Nombre = dto.Nombre,
                DuracionMinutos = dto.DuracionMinutos,
                Precio = dto.Precio,
                Activo = dto.Activo
            };
            _db.Servicios.Add(s);
            await _db.SaveChangesAsync();

            dto.ServicioId = s.ServicioId;
            return CreatedAtAction(nameof(Get), new { id = s.ServicioId }, dto);
        }

        // PUT: api/servicios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ServicioDto dto)
        {
            if (id != dto.ServicioId) return BadRequest();

            var s = await _db.Servicios.FindAsync(id);
            if (s == null) return NotFound();

            s.Nombre = dto.Nombre;
            s.DuracionMinutos = dto.DuracionMinutos;
            s.Precio = dto.Precio;
            s.Activo = dto.Activo;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/servicios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await _db.Servicios.FindAsync(id);
            if (s == null) return NotFound();

            _db.Servicios.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
