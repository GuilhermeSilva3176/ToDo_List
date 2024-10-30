using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace API.Models
{
    [Table("users")]
    public class Users : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}
