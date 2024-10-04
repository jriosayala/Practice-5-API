using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice5API.Data;

namespace Practice5API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity>(AppDbContext context) : ControllerBase where TEntity : class, IEntity
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = ((dynamic)entity).Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity)
        {
            if (((dynamic)entity).Id != id)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntityExists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }
    }
}