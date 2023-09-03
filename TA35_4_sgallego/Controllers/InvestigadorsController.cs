using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TA35_4_sgallego.Models;

namespace TA35_4_sgallego.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigadorsController : ControllerBase
    {
        private readonly FacultadDatabaseContext _context;

        public InvestigadorsController(FacultadDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Investigadors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investigador>>> GetInvestigadors()
        {
          if (_context.Investigadors == null)
          {
              return NotFound();
          }
            return await _context.Investigadors.ToListAsync();
        }

        // GET: api/Investigadors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investigador>> GetInvestigador(string id)
        {
          if (_context.Investigadors == null)
          {
              return NotFound();
          }
            var investigador = await _context.Investigadors.FindAsync(id);

            if (investigador == null)
            {
                return NotFound();
            }

            return investigador;
        }

        // PUT: api/Investigadors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigador(string id, Investigador investigador)
        {
            if (id != investigador.Dni)
            {
                return BadRequest();
            }

            _context.Entry(investigador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigadorExists(id))
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

        // POST: api/Investigadors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investigador>> PostInvestigador(Investigador investigador)
        {
          if (_context.Investigadors == null)
          {
              return Problem("Entity set 'FacultadDatabaseContext.Investigadors'  is null.");
          }
            _context.Investigadors.Add(investigador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvestigadorExists(investigador.Dni))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvestigador", new { id = investigador.Dni }, investigador);
        }

        // DELETE: api/Investigadors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigador(string id)
        {
            if (_context.Investigadors == null)
            {
                return NotFound();
            }
            var investigador = await _context.Investigadors.FindAsync(id);
            if (investigador == null)
            {
                return NotFound();
            }

            _context.Investigadors.Remove(investigador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestigadorExists(string id)
        {
            return (_context.Investigadors?.Any(e => e.Dni == id)).GetValueOrDefault();
        }
    }
}
