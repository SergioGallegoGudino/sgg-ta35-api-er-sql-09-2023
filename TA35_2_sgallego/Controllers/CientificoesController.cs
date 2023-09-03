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
    public class CientificoesController : ControllerBase
    {
        private readonly CientificoDatabaseContext _context;

        public CientificoesController(CientificoDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Cientificoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cientifico>>> GetCientificos()
        {
          if (_context.Cientificos == null)
          {
              return NotFound();
          }
            return await _context.Cientificos.ToListAsync();
        }

        // GET: api/Cientificoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cientifico>> GetCientifico(string id)
        {
          if (_context.Cientificos == null)
          {
              return NotFound();
          }
            var cientifico = await _context.Cientificos.FindAsync(id);

            if (cientifico == null)
            {
                return NotFound();
            }

            return cientifico;
        }

        // PUT: api/Cientificoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCientifico(string id, Cientifico cientifico)
        {
            if (id != cientifico.Dni)
            {
                return BadRequest();
            }

            _context.Entry(cientifico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CientificoExists(id))
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

        // POST: api/Cientificoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cientifico>> PostCientifico(Cientifico cientifico)
        {
          if (_context.Cientificos == null)
          {
              return Problem("Entity set 'CientificoDatabaseContext.Cientificos'  is null.");
          }
            _context.Cientificos.Add(cientifico);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CientificoExists(cientifico.Dni))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCientifico", new { id = cientifico.Dni }, cientifico);
        }

        // DELETE: api/Cientificoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCientifico(string id)
        {
            if (_context.Cientificos == null)
            {
                return NotFound();
            }
            var cientifico = await _context.Cientificos.FindAsync(id);
            if (cientifico == null)
            {
                return NotFound();
            }

            _context.Cientificos.Remove(cientifico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CientificoExists(string id)
        {
            return (_context.Cientificos?.Any(e => e.Dni == id)).GetValueOrDefault();
        }
    }
}
