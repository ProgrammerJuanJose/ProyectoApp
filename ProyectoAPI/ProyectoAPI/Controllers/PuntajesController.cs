using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Attributes;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class PuntajesController : ControllerBase
    {
        private readonly ProyectoFinalContext _context;

        public PuntajesController(ProyectoFinalContext context)
        {
            _context = context;
        }

        // GET: api/Puntajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puntaje>>> GetPuntajes()
        {
          if (_context.Puntajes == null)
          {
              return NotFound();
          }
            return await _context.Puntajes.ToListAsync();
        }

        // GET: api/Puntajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Puntaje>> GetPuntaje(int id)
        {
          if (_context.Puntajes == null)
          {
              return NotFound();
          }
            var puntaje = await _context.Puntajes.FindAsync(id);

            if (puntaje == null)
            {
                return NotFound();
            }

            return puntaje;
        }

        // PUT: api/Puntajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuntaje(int id, Puntaje puntaje)
        {
            if (id != puntaje.PuntajeId)
            {
                return BadRequest();
            }

            _context.Entry(puntaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Puntajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Puntaje>> PostPuntaje(Puntaje puntaje)
        {
          if (_context.Puntajes == null)
          {
              return Problem("Entity set 'ProyectoFinalContext.Puntajes'  is null.");
          }
            _context.Puntajes.Add(puntaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuntaje", new { id = puntaje.PuntajeId }, puntaje);
        }

        // DELETE: api/Puntajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuntaje(int id)
        {
            if (_context.Puntajes == null)
            {
                return NotFound();
            }
            var puntaje = await _context.Puntajes.FindAsync(id);
            if (puntaje == null)
            {
                return NotFound();
            }

            _context.Puntajes.Remove(puntaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuntajeExists(int id)
        {
            return (_context.Puntajes?.Any(e => e.PuntajeId == id)).GetValueOrDefault();
        }
    }
}
