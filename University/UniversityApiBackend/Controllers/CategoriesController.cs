using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Model.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly UniversityDbContext _context;
        private readonly ICategoriesService _categoriesService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(UniversityDbContext context,
                                    ICategoriesService categoriesService,
                                    ILogger<CategoriesController> logger)
        {
            _context = context;
            _categoriesService = categoriesService;
            _logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            _logger.LogInformation($"{nameof(CategoriesController)} - {nameof(GetCategories)}:: RUNNING FUNCTION CALL");

            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            _logger.LogInformation($"{nameof(CategoriesController)} - {nameof(GetCategory)}:: RUNNING FUNCTION CALL");

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            _logger.LogInformation($"{nameof(CategoriesController)} - {nameof(PutCategory)}:: RUNNING FUNCTION CALL");

            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogWarning($"{nameof(CategoriesController)} - {nameof(PutCategory)}:: UNEXPECTED BEHAVIOUR IN FUNCTION CALL");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _logger.LogInformation($"{nameof(CategoriesController)} - {nameof(PostCategory)}:: RUNNING FUNCTION CALL");

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation($"{nameof(CategoriesController)} - {nameof(DeleteCategory)}:: RUNNING FUNCTION CALL");

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            _logger.LogInformation($"{nameof(CategoriesController)} - {nameof(CategoryExists)}:: RUNNING FUNCTION CALL");

            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
