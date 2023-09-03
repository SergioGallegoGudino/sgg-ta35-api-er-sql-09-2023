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
    public class CajeroesController : ControllerBase
    {
        private readonly CajeroDatabaseContext _context;

        public CajeroesController(CajeroDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Cajeroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cajero>>> GetCajeros()
        {
          if (_context.Cajeros == null)
          {
              return NotFound();
          }
            return await _context.Cajeros.ToListAsync();
        }

        // GET: api/Cajeroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cajero>> GetCajero(int id)
        {
          if (_context.Cajeros == null)
          {
              return NotFound();
          }
            var cajero = await _context.Cajeros.FindAsync(id);

            if (cajero == null)
            {
                return NotFound();
            }

            return cajero;
        }

        // PUT: api/Cajeroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCajero(int id, Cajero cajero)
        {
            if (id != cajero.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(cajero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CajeroExists(id))
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

        // POST: api/Cajeroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cajero>> PostCajero(Cajero cajero)
        {
          if (_context.Cajeros == null)
          {
              return Problem("Entity set 'CajeroDatabaseContext.Cajeros'  is null.");
          }
            _context.Cajeros.Add(cajero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CajeroExists(cajero.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCajero", new { id = cajero.Codigo }, cajero);
        }

        // DELETE: api/Cajeroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCajero(int id)
        {
            if (_context.Cajeros == null)
            {
                return NotFound();
            }
            var cajero = await _context.Cajeros.FindAsync(id);
            if (cajero == null)
            {
                return NotFound();
            }

            _context.Cajeros.Remove(cajero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CajeroExists(int id)
        {
            return (_context.Cajeros?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
