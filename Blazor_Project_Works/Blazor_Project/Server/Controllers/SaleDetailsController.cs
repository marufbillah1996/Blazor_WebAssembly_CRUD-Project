using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor_Project.Shared.Models;

namespace Blazor_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailsController : ControllerBase
    {
        private readonly BookDbContext _context;

        public SaleDetailsController(BookDbContext context)
        {
            _context = context;
        }

        // GET: api/SaleDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDetail>>> GetSaleDetails()
        {
          if (_context.SaleDetails == null)
          {
              return NotFound();
          }
            return await _context.SaleDetails.ToListAsync();
        }

        // GET: api/SaleDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDetail>> GetSaleDetail(int id)
        {
          if (_context.SaleDetails == null)
          {
              return NotFound();
          }
            var saleDetail = await _context.SaleDetails.FindAsync(id);

            if (saleDetail == null)
            {
                return NotFound();
            }

            return saleDetail;
        }

        // PUT: api/SaleDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleDetail(int id, SaleDetail saleDetail)
        {
            if (id != saleDetail.SaleID)
            {
                return BadRequest();
            }

            _context.Entry(saleDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleDetailExists(id))
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

        // POST: api/SaleDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaleDetail>> PostSaleDetail(SaleDetail saleDetail)
        {
          if (_context.SaleDetails == null)
          {
              return Problem("Entity set 'BookDbContext.SaleDetails'  is null.");
          }
            _context.SaleDetails.Add(saleDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SaleDetailExists(saleDetail.SaleID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSaleDetail", new { id = saleDetail.SaleID }, saleDetail);
        }

        // DELETE: api/SaleDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleDetail(int id)
        {
            if (_context.SaleDetails == null)
            {
                return NotFound();
            }
            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            _context.SaleDetails.Remove(saleDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleDetailExists(int id)
        {
            return (_context.SaleDetails?.Any(e => e.SaleID == id)).GetValueOrDefault();
        }
    }
}
