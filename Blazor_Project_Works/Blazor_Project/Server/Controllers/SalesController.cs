using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor_Project.Shared.Models;
using Blazor_Project.Shared.DTO;

namespace Blazor_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly BookDbContext _context;

        public SalesController(BookDbContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            return await _context.Sales.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<SaleViewDTO>>> GetSaleDTOs()
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            return await _context.Sales
                .Include(o => o.Customer)
                .Include(o => o.SaleDetails).ThenInclude(oi => oi.Book)
                .Select(o =>
                    new SaleViewDTO
                    {
                        SaleID = o.SaleID,
                        SaleDate = o.SaleDate,
                        
                        CustomerName = o.Customer.CustomerName,
                        Status = o.Status,
                        ItemCount = o.SaleDetails.Count,
                        OrderValue = o.SaleDetails.Sum(x => x.Book.Price * x.Quantity)

                    })
                .ToListAsync();
        }
        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<SaleViewDTO>> GetSaleViewDTO(int id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.Include(o => o.Customer)
                .Include(o => o.SaleDetails).ThenInclude(oi => oi.Book).FirstOrDefaultAsync(o => o.SaleID == id);

            if (sale == null)
            {
                return NotFound();
            }

            return new SaleViewDTO
            {
                SaleID = sale.SaleID,
                SaleDate = sale.SaleDate,
                
                CustomerName = sale.Customer.CustomerName,
                Status = sale.Status,
                ItemCount = sale.SaleDetails.Count,
                OrderValue = sale.SaleDetails.Sum(x => x.Book.Price * x.Quantity)

            };
        }
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<IEnumerable<SaleDetailViewDTO>>> GetSaleDetails(int id)
        {
            if (_context.SaleDetails == null)
            {
                return NotFound();
            }
            var saledetails = await _context.SaleDetails.Include(x => x.Book).Where(oi => oi.SaleID == id).ToListAsync();

            if (saledetails == null)
            {
                return NotFound();
            }

            return saledetails.Select(oi => new SaleDetailViewDTO { SaleID = oi.SaleID, BookName = oi.Book.BookName, Price = oi.Book.Price, Quantity = oi.Quantity }).ToList();
        }
        [HttpGet("OI/{id}")]
        public async Task<ActionResult<IEnumerable<SaleDetail>>> GetOrderItemsOf(int id)
        {
            if (_context.SaleDetails == null)
            {
                return NotFound();
            }
            var saledetails = await _context.SaleDetails.Where(oi => oi.SaleID == id).ToListAsync();

            if (saledetails == null)
            {
                return NotFound();
            }

            return saledetails;
        }
        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.SaleID)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutOrderWithOrderItem(int id, SaleEditDTO sale)
        {
            if (id != sale.SaleID)
            {
                return BadRequest();
            }
            var existing = await _context.Sales.Include(x => x.SaleDetails).FirstAsync(o => o.SaleID == id);
            _context.SaleDetails.RemoveRange(existing.SaleDetails);
            existing.SaleID = sale.SaleID;
            existing.SaleDate = sale.SaleDate;
            existing.CustomerID = sale.CustomerID;
            existing.Status = sale.Status;
            foreach (var item in sale.SaleDetails)
            {
                _context.SaleDetails.Add(new SaleDetail { SaleID = sale.SaleID, BookID = item.BookID, Quantity = item.Quantity });
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException?.Message);

            }

            return NoContent();
        }
        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
          if (_context.Sales == null)
          {
              return Problem("Entity set 'BookDbContext.Sales'  is null.");
          }
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.SaleID }, sale);
        }
        [HttpPost("DTO")]
        public async Task<ActionResult<Sale>> PostSaleDTO(SaleDTO dto)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'ProductDbContext.Orders'  is null.");
            }
            var sale = new Sale { CustomerID = dto.CustomerID, SaleDate = dto.SaleDate,Status = dto.Status };
            foreach (var oi in dto.SaleDetails)
            {
                sale.SaleDetails.Add(new SaleDetail { BookID = oi.BookID, Quantity = oi.Quantity });
            }
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return sale;
        }
        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return (_context.Sales?.Any(e => e.SaleID == id)).GetValueOrDefault();
        }
    }
}
