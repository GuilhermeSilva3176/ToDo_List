using API.Models;
using API.Models.DTOs;
using API.Models.Operations;
using Microsoft.AspNetCore.Http.HttpResults;
using Supabase;


namespace API.Services
{
    public class SupabaseService
    {
        private readonly Client _getClient;

        public SupabaseService(IConfiguration configuration)
        {
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _getClient = new Client(configuration["ConnectionStrings:Url"], configuration["ConnectionStrings:Key"], options);
            try
            {
                _getClient.InitializeAsync().Wait();
            }
            catch (Exception ex)
            {
                BadRequest.ReferenceEquals(ex, null);
            }
        }
        public async Task<List<Todo>> GetTodosAsync()
        {
            var result = await _getClient.From<Todo>().Select(x => new object[] { x.Id, x.Title, x.IsCompleted, x.CreatedAt }).Get();
            return result.Models;
        }


        public async Task<List<Todo>> CreateTodoAsync(Todo model)
        {
            var result = await _getClient.From<Todo>().Insert(model);
            return result.Models;
        }

        public async Task<List<Users>> CreateUsersAsync(Users model)
        {
            var result = await _getClient.From<Users>().Insert(model);
            return result.Models;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await _getClient.From<Users>().Where(x => x.Id == id).Delete();
            return true;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            await _getClient.From<Todo>().Where(x => x.Id == id).Delete();
            return true;
        }

        public async Task<List<Todo>> UpdateTodoAsync(UpdateTodo model, int id)
        {
            var result = await _getClient.From<Todo>().Where(x => x.Id == id).Set(x => x.Title, model.Title).Update();
            return result.Models;
        }

        public async Task<List<Users>> UpdateUserAsync(UserDto model, int id)
        {
            var result = await _getClient.From<Users>().Where(x => x.Id == id).Set(x => x.Email, model.Email).Set(x => x.Password, model.Password).Update();
            return result.Models;
        }

        public async Task<Users?> AuthenticateAsync(string email, string password)
        {
            var result = await _getClient
            .From<Users>()
            .Filter("email", Supabase.Postgrest.Constants.Operator.Equals, email)
            .Filter("password", Supabase.Postgrest.Constants.Operator.Equals, password)
            .Get();
            return result.Models.FirstOrDefault();
        }
    }
}