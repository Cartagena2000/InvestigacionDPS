using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TorneoAPI.Data;
using TorneoAPI.Models;

namespace TorneoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public PartidosController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPartidos()
        {
            var partidos = await _context.Partidos.Find(_ => true).ToListAsync();
            return Ok(partidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartido(string id)
        {
            var partido = await _context.Partidos.Find(p => p.Id == int.Parse(id)).FirstOrDefaultAsync();
            if (partido == null) return NotFound();
            return Ok(partido);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartido([FromBody] Partido partido)
        {
            await _context.Partidos.InsertOneAsync(partido);
            return CreatedAtAction(nameof(GetPartido), new { id = partido.Id.ToString() }, partido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePartido(string id, [FromBody] Partido partidoActualizado)
        {
            var resultado = await _context.Partidos.ReplaceOneAsync(p => p.Id == int.Parse(id), partidoActualizado);
            if (resultado.IsAcknowledged && resultado.ModifiedCount > 0) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartido(string id)
        {
            var resultado = await _context.Partidos.DeleteOneAsync(p => p.Id == int.Parse(id));
            if (resultado.DeletedCount > 0) return NoContent();
            return NotFound();
        }
    }
}
