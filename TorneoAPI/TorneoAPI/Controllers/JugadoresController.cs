using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TorneoAPI.Data;
using TorneoAPI.Models;

namespace TorneoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadoresController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public JugadoresController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetJugadores()
        {
            var jugadores = await _context.Jugadores.Find(_ => true).ToListAsync();
            return Ok(jugadores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJugador(string id)
        {
            var jugador = await _context.Jugadores.Find(j => j.Id == int.Parse(id)).FirstOrDefaultAsync();
            if (jugador == null) return NotFound();
            return Ok(jugador);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJugador([FromBody] Jugador jugador)
        {
            await _context.Jugadores.InsertOneAsync(jugador);
            return CreatedAtAction(nameof(GetJugador), new { id = jugador.Id.ToString() }, jugador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJugador(string id, [FromBody] Jugador jugadorActualizado)
        {
            var resultado = await _context.Jugadores.ReplaceOneAsync(j => j.Id == int.Parse(id), jugadorActualizado);
            if (resultado.IsAcknowledged && resultado.ModifiedCount > 0) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJugador(string id)
        {
            var resultado = await _context.Jugadores.DeleteOneAsync(j => j.Id == int.Parse(id));
            if (resultado.DeletedCount > 0) return NoContent();
            return NotFound();
        }
    }
}
