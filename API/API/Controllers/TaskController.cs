using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public TaskController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            try
            {
                var todos = await _supabaseService.GetTodosAsync();
                return Ok(todos.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<Todo>>> CreateTodo([FromBody] Todo model)
        {   
            try
            {
                var todos = await _supabaseService.CreateTodoAsync(model);
                return CreatedAtAction(nameof(GetTodos), todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}