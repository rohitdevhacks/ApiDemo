using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public ActionResult<IEnumerable<Sales>> GetSales()
        {
            return _context.sales.ToList();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public ActionResult<Sales> GetSales(int id)
        {
            var sales = _context.sales.Find(id);

            if (sales == null)
            {
                return NotFound();
            }

            return sales;
        }

        // POST: api/Sales
        [HttpPost]
        public ActionResult<Sales> PostSales(Sales sales)
        {
            _context.sales.Add(sales);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSales), new { id = sales.Id }, sales);
        }

        // PUT: api/Sales/5
        [HttpPut("{id}")]
        public IActionResult PutSales(int id, Sales sales)
        {
            if (id != sales.Id)
            {
                return BadRequest();
            }

            _context.Entry(sales).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesExists(id))
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

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSales(int id)
        {
            var sales = _context.sales.Find(id);
            if (sales == null)
            {
                return NotFound();
            }

            _context.sales.Remove(sales);
            _context.SaveChanges();

            return NoContent();
        }

        private bool SalesExists(int id)
        {
            return _context.sales.Any(e => e.Id == id);
        }
    }
}
