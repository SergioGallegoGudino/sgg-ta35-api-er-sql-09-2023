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
    public class VentumsController : ControllerBase
    {
        private readonly CajeroDatabaseContext _context;

        public VentumsController(CajeroDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Ventums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ventum>>> GetVenta()
        {
          if (_context.Venta == null)
          {
              return NotFound();
          }
            return await _context.Venta.ToListAsync();
        }

        // GET: api/Ventums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ventum>> GetVentum(int id)
        {
          if (_context.Venta == null)
          {
              return NotFound();
          }
            var ventum = await _context.Venta.FindAsync(id);

            if (ventum == null)
            {
                return NotFound();
            }

            return ventum;
        }

        // PUT: api/Ventums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentum(int id, Ventum ventum)
        {
            if (id != ventum.Cajero)
            {
                return BadRequest();
            }

            _context.Entry(ventum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentumExists(id))
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

        // POST: api/Ventums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ventum>> PostVentum(Ventum ventum)
        {
          if (_context.Venta == null)
          {
              return Problem("Entity set 'CajeroDatabaseContext.Venta'  is null.");
          }
            _context.Venta.Add(ventum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VentumExists(ventum.Cajero))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVentum", new { id = ventum.Cajero }, ventum);
        }

        // DELETE: api/Ventums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentum(int id)
        {
            if (_context.Venta == null)
            {
                return NotFound();
            }
            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }

            _context.Venta.Remove(ventum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentumExists(int id)
        {
            return (_context.Venta?.Any(e => e.Cajero == id)).GetValueOrDefault();
        }
    }
}
