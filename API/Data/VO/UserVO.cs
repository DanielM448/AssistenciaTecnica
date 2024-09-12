using Models;

namespace API.Data.VO
{
    public class UserVO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RoleVO> UserRoles { get; set; } = new List<RoleVO>();
    }
}
