using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class UserRoleModel
    {
        [Column("user_id")]
        public int UserId { get; set; }
        public  UserModel User { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
        public  RoleModel Role { get; set; }
    }
}
