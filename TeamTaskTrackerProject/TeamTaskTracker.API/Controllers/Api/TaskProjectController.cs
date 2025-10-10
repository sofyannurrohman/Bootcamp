using Microsoft.AspNetCore.Mvc;
using TeamTaskTracker.DTOs;
using TeamTaskTracker.Services;

namespace TeamTaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskProjectController : ControllerBase
    {
        private readonly ITaskProjectService _service;

        public TaskProjectController(ITaskProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _service.GetByIdAsync(id);
            if (project == null)
                return NotFound(new { message = $"TaskProject with ID {id} not found." });

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
        }
    }
}
