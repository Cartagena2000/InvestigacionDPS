using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TorneoAPI.Data;
using TorneoAPI.Models;

namespace TorneoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public EquiposController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEquipo(int id)
        {
            // Buscar el equipo por Id
            var equipo = await _context.Equipos.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (equipo == null)
            {
                return NotFound();
            }

            // Buscar los jugadores que pertenecen a el equipo
            var jugadores = await _context.Jugadores.Find(j => j.EquipoId == id).ToListAsync();

            // Asignar los jugadores al equipo
            equipo.Jugadores = jugadores;

            return Ok(equipo);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEquipo([FromBody] Equipo equipo)
        {
            await _context.Equipos.InsertOneAsync(equipo);
            return CreatedAtAction(nameof(GetEquipo), new { id = equipo.Id.ToString() }, equipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipo(string id, [FromBody] Equipo equipoActualizado)
        {
            var resultado = await _context.Equipos.ReplaceOneAsync(e => e.Id == int.Parse(id), equipoActualizado);
            if (resultado.IsAcknowledged && resultado.ModifiedCount > 0) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipo(string id)
        {
            var resultado = await _context.Equipos.DeleteOneAsync(e => e.Id == int.Parse(id));
            if (resultado.DeletedCount > 0) return NoContent();
            return NotFound();
        }
    }
}
