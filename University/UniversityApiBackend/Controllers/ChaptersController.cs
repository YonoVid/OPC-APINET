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
    public class ChaptersController : ControllerBase
    {
        private readonly UniversityDbContext _context;
        private readonly IChaptersService _chaptersService;
        private readonly ILogger<ChaptersController> _logger;

        public ChaptersController(UniversityDbContext context,
                                    IChaptersService chaptersService,
                                    ILogger<ChaptersController> logger)
        {
            _context = context;
            _chaptersService = chaptersService;
            _logger = logger;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapters>>> GetChapters()
        {
            _logger.LogInformation($"{nameof(ChaptersController)} - {nameof(GetChapters)}:: RUNNING FUNCTION CALL");

            return await _context.Chapters.ToListAsync();
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chapters>> GetChapters(int id)
        {
            _logger.LogInformation($"{nameof(ChaptersController)} - {nameof(GetChapters)}:: RUNNING FUNCTION CALL");

            var chapters = await _context.Chapters.FindAsync(id);

            if (chapters == null)
            {
                return NotFound();
            }

            return chapters;
        }

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutChapters(int id, Chapters chapters)
        {
            _logger.LogInformation($"{nameof(ChaptersController)} - {nameof(PutChapters)}:: RUNNING FUNCTION CALL");

            if (id != chapters.Id)
            {
                return BadRequest();
            }

            _context.Entry(chapters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChaptersExists(id))
                {
                    return NotFound();
                }
                else
                {

                    _logger.LogWarning($"{nameof(ChaptersController)} - {nameof(PutChapters)}:: UNEXPECTED BEHAVIOUR IN FUNCTION CALL");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Chapters>> PostChapters(Chapters chapters)
        {
            _logger.LogInformation($"{nameof(ChaptersController)} - {nameof(PostChapters)}:: RUNNING FUNCTION CALL");

            _context.Chapters.Add(chapters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChapters", new { id = chapters.Id }, chapters);
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteChapters(int id)
        {
            _logger.LogInformation($"{nameof(ChaptersController)} - {nameof(DeleteChapters)}:: RUNNING FUNCTION CALL");

            var chapters = await _context.Chapters.FindAsync(id);
            if (chapters == null)
            {
                return NotFound();
            }

            _context.Chapters.Remove(chapters);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChaptersExists(int id)
        {
            _logger.LogInformation($"{nameof(ChaptersController)} - {nameof(ChaptersExists)}:: RUNNING FUNCTION CALL");

            return _context.Chapters.Any(e => e.Id == id);
        }
    }
}
