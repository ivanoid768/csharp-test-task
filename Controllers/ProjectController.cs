using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            return await _context.Projects.Select(x => ProjectToDTO(x)).ToListAsync();
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return ProjectToDTO(project);
        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, CreateProjectDTO projectDTO)
        {
            var project = new Project
            {
                Id = id,
                Name = projectDTO.Name,
                StartDate = projectDTO.StartDate,
                CompletitionDate = projectDTO.CompletitionDate,
                Status = projectDTO.Status,
                Priority = projectDTO.Priority
            };

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> PostProject(CreateProjectDTO projectDTO)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'DataContext.Projects'  is null.");
            }

            var project = new Project
            {
                Name = projectDTO.Name,
                StartDate = projectDTO.StartDate,
                CompletitionDate = projectDTO.CompletitionDate,
                Status = projectDTO.Status,
                Priority = projectDTO.Priority,
                tasks = new List<ProjectTask>()
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, ProjectToDTO(project));
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("/filter")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetFilteredProjects(FilterProjectDTO filter)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }

            return await _context.Projects
                .Where(project => project.StartDate >= filter.StartDateFrom
                    & project.StartDate <= filter.StartDateTo)
                .Select(x => ProjectToDTO(x)).ToListAsync();
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static ProjectDTO ProjectToDTO(Project project) =>
            new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                StartDate = project.StartDate,
                CompletitionDate = project.CompletitionDate,
                Status = project.Status,
                Priority = project.Priority
            };
    }
}
