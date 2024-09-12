using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("users")]
    public class UserModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_name")]
        public string Name { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        public string Password { get; set; } = string.Empty;
        [Column("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [Column("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
        [Column("deleted")]
        public DateTime Deleted { get; set; } = default(DateTime);
        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
    }
}
