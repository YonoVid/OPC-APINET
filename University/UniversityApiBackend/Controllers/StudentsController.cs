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
    public class StudentsController : ControllerBase
    {
        private readonly UniversityDbContext _context;
        private readonly IStudentsService _studentServices;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(UniversityDbContext context,
                                    IStudentsService studentsService,
                                    ILogger<StudentsController> logger)
        {
            _context = context;
            _studentServices = studentsService;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(GetStudents)}:: RUNNING FUNCTION CALL");

            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/GetStudentsWithCourses
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsWithCourses()
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(GetStudentCourses)}:: RUNNING FUNCTION CALL");


            return Ok(await _studentServices.AsyncGetStudentsWithCourses(_context.Students));

        }

        // GET: api/Students/GetStudentsWithoutCourses
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsWithoutCourses()
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(GetStudentCourses)}:: RUNNING FUNCTION CALL");

            return Ok(await _studentServices.AsyncGetStudentsWithNoCourses(_context.Students));

        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(GetStudent)}:: RUNNING FUNCTION CALL");

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // GET: api/Students/GetStudentCourses/5
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentCourses(int id)
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(GetStudentCourses)}:: RUNNING FUNCTION CALL");

            return Ok(await _studentServices.AsyncGetAllCoursesOfStudent(_context.Students, id));
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(PutStudent)}:: RUNNING FUNCTION CALL");

            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogWarning($"{nameof(StudentsController)} - {nameof(GetStudent)}:: UNEXPECTED BEHAVIOUR IN FUNCTION CALL");

                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(PostStudent)}:: RUNNING FUNCTION CALL");

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(DeleteStudent)}:: RUNNING FUNCTION CALL");

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            _logger.LogInformation($"{nameof(StudentsController)} - {nameof(StudentExists)}:: RUNNING FUNCTION CALL");

            return _context.Students.Any(e => e.Id == id);
        }
    }
}
