using API.Models;
using API.Models.Operations;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(SupabaseService supabaseService) : ControllerBase
    {
        private readonly SupabaseService _supabaseService = supabaseService;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoAsync(int id)
        {
            try
            {
                var success = await _supabaseService.DeleteTodoAsync(id);
                if (success)
                {
                    return Ok("To-do excluído com sucesso");
                }
                else
                {
                    return NotFound("To-do não encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoAsync(int id,  [FromBody] UpdateTodo model)
        {
            var resultado = await _supabaseService.UpdateTodoAsync(model, id);
            return Ok(resultado);
        }
    }
}