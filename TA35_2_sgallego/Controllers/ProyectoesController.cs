using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TA35_2_sgallego.Models;

namespace TA35_2_sgallego.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoesController : ControllerBase
    {
        private readonly CientificoDatabaseContext _context;

        public ProyectoesController(CientificoDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Proyectoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetProyectos()
        {
          if (_context.Proyectos == null)
          {
              return NotFound();
          }
            return await _context.Proyectos.ToListAsync();
        }

        // GET: api/Proyectoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyecto(string id)
        {
          if (_context.Proyectos == null)
          {
              return NotFound();
          }
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        // PUT: api/Proyectoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(string id, Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return BadRequest();
            }

            _context.Entry(proyecto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
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

        // POST: api/Proyectoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto proyecto)
        {
          if (_context.Proyectos == null)
          {
              return Problem("Entity set 'CientificoDatabaseContext.Proyectos'  is null.");
          }
            _context.Proyectos.Add(proyecto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProyectoExists(proyecto.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProyecto", new { id = proyecto.Id }, proyecto);
        }

        // DELETE: api/Proyectoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(string id)
        {
            if (_context.Proyectos == null)
            {
                return NotFound();
            }
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProyectoExists(string id)
        {
            return (_context.Proyectos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
