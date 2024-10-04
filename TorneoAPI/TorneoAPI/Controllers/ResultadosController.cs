using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TorneoAPI.Data;
using TorneoAPI.Models;

namespace TorneoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public ResultadosController(MongoDbContext context)
        {
            _context = context;
        }

        // GET: api/resultados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resultado>>> GetResultados()
        {
            var resultados = await _context.Resultados.Find(r => true).ToListAsync();
            return Ok(resultados);
        }

        // GET: api/resultados/{id}
        [HttpGet("{partidoId:int}")]
        public async Task<ActionResult<Resultado>> GetResultado(int partidoId)
        {
            var resultado = await _context.Resultados.Find(r => r.PartidoId == partidoId).FirstOrDefaultAsync();
            if (resultado == null)
            {
                return NotFound();
            }
            return Ok(resultado);
        }

        // POST: api/resultados
        [HttpPost]
        public async Task<ActionResult<Resultado>> PostResultado(Resultado resultado)
        {
            await _context.Resultados.InsertOneAsync(resultado);
            return CreatedAtAction(nameof(GetResultado), new { partidoId = resultado.PartidoId }, resultado);
        }

        // PUT: api/resultados/{id}
        [HttpPut("{partidoId:int}")]
        public async Task<IActionResult> PutResultado(int partidoId, Resultado resultado)
        {
            if (partidoId != resultado.PartidoId)
            {
                return BadRequest();
            }

            var result = await _context.Resultados.ReplaceOneAsync(r => r.PartidoId == partidoId, resultado);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/resultados/{id}
        [HttpDelete("{partidoId:int}")]
        public async Task<IActionResult> DeleteResultado(int partidoId)
        {
            var result = await _context.Resultados.DeleteOneAsync(r => r.PartidoId == partidoId);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
