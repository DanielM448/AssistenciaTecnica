using API.Data.VO;
using Models;

namespace API.Repositories.User
{
    public interface IUserRepository
    {
        void Add(UserModel user);
        UserModel? GetByEmail(string email);
        void Update(UserModel user);
        UserModel? ValidateCredentials(LoginVO user);
        UserModel? ValidateCredentials(string email);
        UserModel RefreshUserInfo(UserModel user);
        UserModel UserRoleAdd(string email, RoleModel role);
        public UserModel UserRoleRemove(string email, RoleModel role);
        bool RevokeToken(string email);
        List<UserModel> GetAll();

        // TODO - Login Google
        // TODO - Login Microsoft
    }
}
