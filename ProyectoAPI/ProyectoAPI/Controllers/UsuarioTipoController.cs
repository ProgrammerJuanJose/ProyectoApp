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
    public class UsuarioTipoController : ControllerBase
    {
        private readonly ProyectoFinalContext _context;

        public UsuarioTipoController(ProyectoFinalContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioTipoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioTipo>>> GetUsuarioTipos()
        {
          if (_context.UsuarioTipos == null)
          {
              return NotFound();
          }
            return await _context.UsuarioTipos.ToListAsync();
        }

        // GET: api/UsuarioTipoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioTipo>> GetUsuarioTipo(int id)
        {
          if (_context.UsuarioTipos == null)
          {
              return NotFound();
          }
            var usuarioTipo = await _context.UsuarioTipos.FindAsync(id);

            if (usuarioTipo == null)
            {
                return NotFound();
            }

            return usuarioTipo;
        }

        // PUT: api/UsuarioTipoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioTipo(int id, UsuarioTipo usuarioTipo)
        {
            if (id != usuarioTipo.UsuarioTipoId)
            {
                return BadRequest();
            }

            _context.Entry(usuarioTipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioTipoExists(id))
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

        // POST: api/UsuarioTipoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioTipo>> PostUsuarioTipo(UsuarioTipo usuarioTipo)
        {
          if (_context.UsuarioTipos == null)
          {
              return Problem("Entity set 'ProyectoFinalContext.UsuarioTipos'  is null.");
          }
            _context.UsuarioTipos.Add(usuarioTipo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioTipoExists(usuarioTipo.UsuarioTipoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuarioTipo", new { id = usuarioTipo.UsuarioTipoId }, usuarioTipo);
        }

        // DELETE: api/UsuarioTipoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioTipo(int id)
        {
            if (_context.UsuarioTipos == null)
            {
                return NotFound();
            }
            var usuarioTipo = await _context.UsuarioTipos.FindAsync(id);
            if (usuarioTipo == null)
            {
                return NotFound();
            }

            _context.UsuarioTipos.Remove(usuarioTipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioTipoExists(int id)
        {
            return (_context.UsuarioTipos?.Any(e => e.UsuarioTipoId == id)).GetValueOrDefault();
        }
    }
}
