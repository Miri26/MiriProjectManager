using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiriProjectManager.Server.DTOs;
using MiriProjectManager.Server.Data;

namespace MiriProjectManager.Server.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly LocalDBContext _context;

        public ProjectController(LocalDBContext context)
        {
            _context = context;
        }

        // GET: api/ProjectDTOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjectDTO(string username)
        {
            return _context.ProjectDTO.Where(p => p.Username == username).ToList();
        }

        // GET: api/ProjectDTOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProjectDTO(int id)
        {
            var projectDTO = await _context.ProjectDTO.FindAsync(id);

            if (projectDTO == null)
            {
                return NotFound();
            }

            return projectDTO;
        }

        // POST: api/ProjectDTOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> PostProjectDTO(ProjectDTO projectDTO)
        {
            _context.ProjectDTO.Add(projectDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectDTO), new { id = projectDTO.Id }, projectDTO);
        }

        // DELETE: api/ProjectDTOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectDTO(int id)
        {
            var projectDTO = await _context.ProjectDTO.FindAsync(id);
            if (projectDTO == null)
            {
                return NotFound();
            }

            _context.ProjectDTO.Remove(projectDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
