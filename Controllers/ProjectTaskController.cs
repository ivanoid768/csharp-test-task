using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectTaskController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTask
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTaskDTO>>> GetTasks()
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            return await _context.Tasks.Select(task => TaskToDTO(task)).ToListAsync();
        }

        // GET: api/ProjectTask/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskDTO>> GetProjectTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var projectTask = await _context.Tasks.FindAsync(id);

            if (projectTask == null)
            {
                return NotFound();
            }

            return TaskToDTO(projectTask);
        }

        // PUT: api/ProjectTask/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask(int id, ProjectTaskDTO projectTaskDTO)
        {
            if (id != projectTaskDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectTaskDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(id))
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

        // POST: api/ProjectTask/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{projectId}")]
        public async Task<ActionResult<ProjectTask>> PostProjectTask(int projectId, CreateProjectTaskDTO projectTaskDTO)
        {
            // if (_context.Tasks == null)
            // {
            //     return Problem("Entity set 'DataContext.Tasks'  is null.");
            // }

            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            var projectTask = new ProjectTask
            {
                Name = projectTaskDTO.Name,
                Status = projectTaskDTO.Status,
                Description = projectTaskDTO.Description,
                Priority = projectTaskDTO.Priority
            };

            project.tasks.Add(projectTask);

            _context.Tasks.Add(projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectTask), new { id = projectTask.Id }, projectTask);
        }

        // DELETE: api/ProjectTask/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var projectTask = await _context.Tasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(projectTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectTaskExists(int id)
        {
            return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: api/ProjectTask
        [HttpGet("{projectid}")]
        public async Task<ActionResult<IEnumerable<ProjectTaskDTO>>> GetProjectTasks(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Where(t => t.Project == project)
                .Select(task => TaskToDTO(task))
                .ToListAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
        }

        private ProjectTaskDTO TaskToDTO(ProjectTask task)
        {
            return new ProjectTaskDTO
            {
                Id = task.Id,
                Description = task.Description,
                Name = task.Name,
                Priority = task.Priority,
                Status = task.Status
            };
        }
    }
}
