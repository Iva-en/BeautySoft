using BeautySoft.Domain.Entities;
using BeautySoft.Infrastructure.Data;
using BeautySoft.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySoft.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly BeautySoftDbContext _db;
        public CitasController(BeautySoftDbContext db) => _db = db;

        // GET: api/citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetAll()
        {
            var data = await _db.Citas
                .OrderBy(c => c.FechaHora)
                .Select(c => new CitaDto
                {
                    CitaId = c.CitaId,
                    ClienteId = c.ClienteId,
                    EstilistaId = c.EstilistaId,
                    ServicioId = c.ServicioId,
                    FechaHora = c.FechaHora,
                    Estado = c.Estado.ToString(),
                    PrecioFinal = c.PrecioFinal,
                    Notas = c.Notas
                })
                .ToListAsync();
            return Ok(data);
        }

        // GET: api/citas/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CitaDto>> Get(int id)
        {
            var c = await _db.Citas.FindAsync(id);
            if (c == null) return NotFound();

            var dto = new CitaDto
            {
                CitaId = c.CitaId,
                ClienteId = c.ClienteId,
                EstilistaId = c.EstilistaId,
                ServicioId = c.ServicioId,
                FechaHora = c.FechaHora,
                Estado = c.Estado.ToString(),
                PrecioFinal = c.PrecioFinal,
                Notas = c.Notas
            };
            return Ok(dto);
        }

        // POST: api/citas
        [HttpPost]
        public async Task<ActionResult<CitaDto>> Post(CitaDto dto)
        {
            var cita = new Cita
            {
                ClienteId = dto.ClienteId,
                EstilistaId = dto.EstilistaId,
                ServicioId = dto.ServicioId,
                FechaHora = dto.FechaHora,
                Estado = Enum.TryParse<EstadoCita>(dto.Estado, out var est) ? est : EstadoCita.Programada,
                PrecioFinal = dto.PrecioFinal,
                Notas = dto.Notas
            };

            _db.Citas.Add(cita);
            await _db.SaveChangesAsync();

            dto.CitaId = cita.CitaId;
            dto.Estado = cita.Estado.ToString();
            return CreatedAtAction(nameof(Get), new { id = cita.CitaId }, dto);
        }

        // PUT: api/citas/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CitaDto dto)
        {
            if (id != dto.CitaId) return BadRequest();

            var c = await _db.Citas.FindAsync(id);
            if (c == null) return NotFound();

            c.ClienteId = dto.ClienteId;
            c.EstilistaId = dto.EstilistaId;
            c.ServicioId = dto.ServicioId;
            c.FechaHora = dto.FechaHora;
            c.Estado = Enum.TryParse<EstadoCita>(dto.Estado, out var est) ? est : c.Estado;
            c.PrecioFinal = dto.PrecioFinal;
            c.Notas = dto.Notas;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/citas/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Citas.FindAsync(id);
            if (c == null) return NotFound();

            _db.Citas.Remove(c);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
