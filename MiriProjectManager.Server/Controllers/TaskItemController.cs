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
    [Route("api/tasks/")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly LocalDBContext _context;

        public TaskItemController(LocalDBContext context)
        {
            _context = context;
        }

        // GET: api/TaskItemDTOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDTO>>> GetTaskItemDTO()
        {
            return await _context.TaskItemDTO.ToListAsync();
        }

        // GET: api/TaskItemDTOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDTO>> GetTaskItemDTO(int id)
        {
            var taskItemDTO = await _context.TaskItemDTO.FindAsync(id);

            if (taskItemDTO == null)
            {
                return NotFound();
            }

            return taskItemDTO;
        }

        // PUT: api/TaskItemDTOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItemDTO(int id, TaskItemDTO taskItemDTO)
        {
            if (id != taskItemDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItemDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemDTOExists(id))
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

        // POST: api/TaskItemDTOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskItemDTO>> PostTaskItemDTO(TaskItemDTO taskItemDTO)
        {
            _context.TaskItemDTO.Add(taskItemDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskItemDTO", new { id = taskItemDTO.Id }, taskItemDTO);
        }

        // DELETE: api/TaskItemDTOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItemDTO(int id)
        {
            var taskItemDTO = await _context.TaskItemDTO.FindAsync(id);
            if (taskItemDTO == null)
            {
                return NotFound();
            }

            _context.TaskItemDTO.Remove(taskItemDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskItemDTOExists(int id)
        {
            return _context.TaskItemDTO.Any(e => e.Id == id);
        }
    }
}
