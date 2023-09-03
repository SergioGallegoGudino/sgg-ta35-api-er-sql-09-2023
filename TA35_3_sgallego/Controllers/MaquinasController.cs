using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TA35_3_sgallego.Models;

namespace TA35_3_sgallego.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaquinasController : ControllerBase
    {
        private readonly CajeroDatabaseContext _context;

        public MaquinasController(CajeroDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Maquinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquina>>> GetMaquinas()
        {
          if (_context.Maquinas == null)
          {
              return NotFound();
          }
            return await _context.Maquinas.ToListAsync();
        }

        // GET: api/Maquinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquina>> GetMaquina(int id)
        {
          if (_context.Maquinas == null)
          {
              return NotFound();
          }
            var maquina = await _context.Maquinas.FindAsync(id);

            if (maquina == null)
            {
                return NotFound();
            }

            return maquina;
        }

        // PUT: api/Maquinas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(int id, Maquina maquina)
        {
            if (id != maquina.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(maquina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(id))
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

        // POST: api/Maquinas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maquina>> PostMaquina(Maquina maquina)
        {
          if (_context.Maquinas == null)
          {
              return Problem("Entity set 'CajeroDatabaseContext.Maquinas'  is null.");
          }
            _context.Maquinas.Add(maquina);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaquinaExists(maquina.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaquina", new { id = maquina.Codigo }, maquina);
        }

        // DELETE: api/Maquinas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            if (_context.Maquinas == null)
            {
                return NotFound();
            }
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return NotFound();
            }

            _context.Maquinas.Remove(maquina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaquinaExists(int id)
        {
            return (_context.Maquinas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
