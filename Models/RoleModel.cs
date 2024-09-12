using Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("roles")]
    public class RoleModel : BaseEntity
    {
        [Column("name")]
        public string RoleName { get; set; } = string.Empty;
        public List<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
    }
}
