using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace API.Models
{
    [Table("tasks")]
    public class Todo : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }
    }
}
