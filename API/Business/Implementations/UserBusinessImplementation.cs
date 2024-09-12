using API.Data.Converter.Implementations;
using API.Data.VO;
using API.Libraries.Validations;
using API.Repositories.User;
using Models;

namespace API.Business.Implementations
{
    public class UserBusinessImplementation : IUserBusiness
    {
        private readonly IUserRepository _repository;
        private readonly UserConverter _converter;
        private readonly RoleConverter _converterRole;
        public UserBusinessImplementation(IUserRepository repository)
        {
            _repository = repository;
            _converter = new UserConverter();
            _converterRole = new RoleConverter();
        }

        public UserVO GetUserByEmail(string email)
        {
            var user = _repository.GetByEmail(email);

            return SetUserRoles(user);
        }

        private UserVO SetUserRoles(UserModel? user)
        {
            var userRoles = user != null ? GetUserRoles(user) : new List<RoleVO>();
            var userVO = _converter.Parse(user);
            userVO.UserRoles = userRoles;
            return userVO;
        }

        public List<UserVO> GetUsers()
        {
            var users = _repository.GetAll();
            return users.Select(SetUserRoles).ToList();

        }

        public void RegisterUser(RegisterUserVO user)
        {
            UserModel userSave = new UserModel
            {
                Name = user.UserName,
                Email = user.Email,
                Password = user.Password,
            };
            _repository.Add(userSave);
        }      
        public UserVO AddRole(string email, RoleVO role)
        {
            var user = _repository.GetByEmail(email);
            if (user == null) return null;

            var roleModel = _converterRole.Parse(role);

            user = _repository.UserRoleAdd(email, roleModel);

            return SetUserRoles(user); ;
        }
        public UserVO RemoveRole(string email, RoleVO role)
        {
            var user = _repository.GetByEmail(email);
            if (user == null) return null;

            var roleModel = _converterRole.Parse(role);

            user = _repository.UserRoleRemove(email, roleModel);

            return SetUserRoles(user); ;
        }
        
        public bool ValidateUserExist(string email)
        {
            return _repository.GetByEmail(email) != null;
        }

        public string? IsvalidRequirementsRegisterUserVO(RegisterUserVO user)
        {
            if (user == null) return "Invalid client request";

            if (!EmailValidate.IsValidEmail(user.Email)) return "Invalid Email";

            if (!PasswordValidateRequirements.IsValid(user.Password)) return PasswordValidateRequirements.RequirementsString();

            if (!PasswordValidateRequirements.PasswordConfirmationIsEqual(user.Password, user.PasswordConfirmation)) return "Passwords do not match. Please check and try again.";

            if (ValidateUserExist(user.Email)) return "This email is already taken. Please log in.";

            if (user.UserName == null || user.UserName.Length < 3) return "The userName field must contain a minimum of 3 characters";

            return null;
        }

        private List<RoleVO> GetUserRoles(UserModel? user)
        {
            return user?.UserRoles?.Select(role => role.Role).Select(_converterRole.Parse).ToList() ?? new List<RoleVO>();
        }

        
    }
}
