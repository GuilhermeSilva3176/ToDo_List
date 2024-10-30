using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace API.Models.Operations
{
    public class UpdateTodo
    {

        [Column("title")]
        public string? Title { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }

    }
}
