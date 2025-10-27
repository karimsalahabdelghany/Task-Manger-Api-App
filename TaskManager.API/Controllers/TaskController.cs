using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.DTOS;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public TaskController(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _appDb.Tasks.Where(t => t.UserId == UserId).ToList();
            return Ok(tasks);
        }
        [HttpPost]
        public async Task<IActionResult> CraetTask([FromBody] CreateTaskDto taskDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("No UserId in token");

            var userExists = await _appDb.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                return NotFound("User not found");


            var task = new Taskitem
            {
                Title = taskDto.Title,
                IsCompleted = taskDto.IsCompleted,
                UserId = userId
            };

            _appDb.Tasks.Add(task);
            await _appDb.SaveChangesAsync();

            return Ok(task);

        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] CreateTaskDto dto) // TaskCreateDto: Title + IsCompleted
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            if (dto == null)
                return BadRequest(new { error = "Body is null" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            var task = await _appDb.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null) return NotFound();

            task.Title = dto.Title ?? task.Title;
            task.IsCompleted = dto.IsCompleted;

            try
            {
                await _appDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

            return Ok(task);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            // Get user id from token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // No user id in token -> Not authenticated properly
                return Unauthorized(new { message = "User not authenticated" });
            }

            try
            {
                var task = await _appDb.Tasks.FindAsync(id);
                if (task == null)
                    return NotFound(new { message = "Task not found" });

                if (task.UserId != userId)
                    return Forbid(); // أو return NotFound() لإخفاء وجود الـ id للمستخدم الآخر

                _appDb.Tasks.Remove(task);
                await _appDb.SaveChangesAsync();

                return Ok(new { message = "Deleted" });
            }
            catch (DbUpdateException dbEx)
            {
                // Database update exception (FK constraint)
               
                return StatusCode(500, new { message = "Database update failed", detail = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }

    }
    


}
