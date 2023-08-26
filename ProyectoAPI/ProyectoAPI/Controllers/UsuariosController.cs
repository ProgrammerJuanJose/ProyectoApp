using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Attributes;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class UsuariosController : ControllerBase
    {
        private readonly ProyectoFinalContext _context;

        public UsuariosController(ProyectoFinalContext context)
        {
            _context = context;
        }

        [HttpGet("ValidateLogin")]
        public async Task<ActionResult<Usuario>> ValidateLogin(string Correo, string Contrasennia)
        {
            var user = await _context.Usuarios.SingleOrDefaultAsync(e => e.UsuarioCorreo.Equals(Correo) &&
                                                                 e.UsuarioContrasennia == Contrasennia);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("GetUserInfoByEmail")]
        public ActionResult<IEnumerable<Usuario>> GetUserInfoByEmail(string PUsuarioCorreo)
        {
            //acá creamos un linq que combina información de dos entidades 
            //(user inner join userrole) y la agreaga en el objeto dto de usuario 

            var query = (from u in _context.Usuarios
                         join ur in _context.UsuarioTipos on
                         u.UsuarioId equals ur.UsuarioTipoId
                         where u.UsuarioCorreo == PUsuarioCorreo && u.Activo == true
                         select new
                         {
                             idusuario = u.UsuarioId,
                             correo = u.UsuarioCorreo,
                             contrasennia = u.UsuarioContrasennia,
                             nombre = u.UsuarioNombre,
                             cedula = u.UsuarioCedula,
                             telefono = u.UsuarioTelefono,
                             direccion = u.UsuarioDireccion,
                             activo = u.Activo,
                             idTipo= ur.UsuarioTipoId,
                             descripcionTipo = ur.UsuarioTipoDescripcion
                         }).ToList();

            //creamos un objeto del tipo que retorna la función 
            List<Usuario> list = new List<Usuario>();

            foreach (var item in query)
            {
                Usuario NewItem = new Usuario()
                {
                    UsuarioId = item.idusuario,
                    UsuarioCorreo = item.correo,
                    UsuarioContrasennia = item.contrasennia,
                    UsuarioNombre = item.nombre,
                    UsuarioCedula = item.cedula,
                    UsuarioTelefono = item.telefono,
                    UsuarioDireccion = item.direccion,
                    Activo = item.activo
                };

                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }

            return list;

        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Usuario user)
        {
            if (id != user.UsuarioId)
            {
                return BadRequest();
            }


            Usuario? NewEFUser = GetUserByID(id);

            if (NewEFUser != null)
            {
                NewEFUser.UsuarioCorreo = user.UsuarioCorreo;
                NewEFUser.UsuarioNombre = user.UsuarioNombre;
                NewEFUser.UsuarioCedula = user.UsuarioCedula;
                NewEFUser.UsuarioTelefono = user.UsuarioTelefono;
                NewEFUser.UsuarioDireccion = user.UsuarioDireccion;

                _context.Entry(NewEFUser).State = EntityState.Modified;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'ProyectoFinalContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        private bool UserExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }

        private Usuario? GetUserByID(int id)
        {
            var user = _context.Usuarios.Find(id);

            //var user = _context.Users?.Any(e => e.UserId == id);

            return user;
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
    }
}
