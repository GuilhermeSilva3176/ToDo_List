using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace API.Models.DTOs
{
    public class UserDto
    {
        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}
