using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using sms_portal_backend.Entities;


namespace sms_portal_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private DbContext db;
        public CategoryController(smsportalContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await db.Set<Category>().ToArrayAsync<Category>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(long id)
        {
            var category = await db.FindAsync<Category>(id);
            if (category == null) return NotFound();
            return category;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await db.AddAsync<Category>(category);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Category category)
        {
            var categoryCurrent = await db.FindAsync<Category>(id);
            if (categoryCurrent == null) return NotFound();
            categoryCurrent.Name = category.Name;
            categoryCurrent.Label = category.Label;
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await db.FindAsync<Category>(id);
            if (category == null) return NotFound();
            db.Remove<Category>(category);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}