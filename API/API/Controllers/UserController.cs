using API.Models;
using API.Models.DTOs;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SupabaseService _service;

        public UserController(SupabaseService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<List<Users>>> CreateUser([FromBody] Users user)
        {
            try
            {
                var createUser = await _service.CreateUsersAsync(user);
                return Ok("Usuario Criado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUserData([FromBody] UserDto updateRequest, int id)
        {
            try
            {
                var updateUser = await _service.UpdateUserAsync(updateRequest, id);
                return Ok("Dados atualizados com sucesso");
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _service.DeleteUserAsync(id);
            if (success)
                return Ok("Conta deletada com sucesso");

            return BadRequest("Erro ao deletar conta");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginRequest)
        {
            var user = await _service.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            return Ok(new { message = "Login realizado com sucesso", user });
        }
    }
}
