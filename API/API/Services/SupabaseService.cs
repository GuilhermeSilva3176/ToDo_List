using API.Models;
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
    }
}