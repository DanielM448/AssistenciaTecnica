using API.Data.VO;
using Models;

namespace API.Business
{
    public interface IUserBusiness
    {
        bool ValidateUserExist(string email);
        void RegisterUser(RegisterUserVO user);
        UserVO GetUserByEmail(string email);
        List<UserVO> GetUsers();
        UserVO AddRole(string email, RoleVO role);
        UserVO RemoveRole(string email, RoleVO role);
        string? IsvalidRequirementsRegisterUserVO(RegisterUserVO user);

    }
}
