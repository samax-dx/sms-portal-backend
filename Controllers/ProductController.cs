using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text.Json;

using sms_portal_backend.Entities;


namespace sms_portal_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private DbContext db;
        public ProductController(smsportalContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement task)
        {
            try {
                var id = task.GetProperty("id").GetInt64();
                var content = await db.FindAsync<Product>(id);
                return new JsonResult(content);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return await Task.Factory.StartNew<IActionResult>(() => NoContent());
        }

        // [HttpGet]
        // public async Task<IEnumerable<Product>> Get()
        // {
        //     return await db.Set<Product>().ToArrayAsync<Product>();
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Product>> Get(long id)
        // {
        //     var product = await db.FindAsync<Product>(id);
        //     if (product == null) return NotFound();
        //     return product;
        // }

        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody] Product product)
        // {
        //     await db.AddAsync<Product>(product);
        //     await db.SaveChangesAsync();
        //     return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(long id, [FromBody] Product product)
        // {
        //     var productCurrent = await db.FindAsync<Product>(id);
        //     if (productCurrent == null) return NotFound();
        //     productCurrent.Name = product.Name;
        //     productCurrent.Category = product.Category;
        //     await db.SaveChangesAsync();
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(long id)
        // {
        //     var category = await db.FindAsync<Product>(id);
        //     if (category == null) return NotFound();
        //     db.Remove<Product>(category);
        //     await db.SaveChangesAsync();
        //     return NoContent();
        // }
    }
}