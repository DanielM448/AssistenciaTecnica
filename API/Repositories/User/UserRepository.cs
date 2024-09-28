using API.Data.VO;
using API.Db.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace API.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        

        public UserRepository(MySQLContext context)
        {
            _context = context;
            
        }
        public UserModel? ValidateCredentials(string email)
        {
            return _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Email == email);
        }
        public UserModel? ValidateCredentials(LoginVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => (u.Email == user.Email) && (u.Password == pass));
        }
        public void Add(UserModel user)
        {
            user.Password = ComputeHash(user.Password, SHA256.Create());
            _context.Users.Add(user);
            _context.SaveChanges();
            UserFirstRoleAdd(user);
        }
        public UserModel? GetByEmail(string email)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Email == email);
            return user;
        }

        public List<UserModel> GetAll()
        {
            var users = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToList();
            return users;
        }
        public void Update(UserModel user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public UserModel RefreshUserInfo(UserModel user)
        {
            var result = _context.Users.SingleOrDefault(p => p.Id == user.Id);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            return result;
        }

        public bool RevokeToken(string email)
        {
            var user = _context.Users.SingleOrDefault(u => (u.Email == email));
            if (user == null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return string.Concat(hashedBytes.Select(b => b.ToString("x2")));
        }

        private bool FirstUser()
        {
            return _context.Users.Count() <= 1;
        }

        private RoleModel? UserRoleType()
        {
            if (FirstUser())
            {
                return _context.Roles.FirstOrDefault(r => (r.RoleName == "Admin"));
            }
            else
            {
                return _context.Roles.FirstOrDefault(r => (r.RoleName == "Client"));
            }
        }

        private void UserFirstRoleAdd(UserModel user)
        {
            var role = UserRoleType();
            if (role != null)
            {
                UserRoleAdd(user.Email, role);
            }
        }

        public UserModel UserRoleAdd(string email, RoleModel role)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                var existingRole = _context.Roles.Local.FirstOrDefault(r => r.Id == role.Id);
                if (existingRole == null)
                {
                    _context.Roles.Attach(role);
                }
                else
                {
                    role = existingRole;
                }
                var existingUserRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == role.Id);
                if (existingUserRole == null)
                {
                    var userRole = new UserRoleModel
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                        User = user,
                        Role = role
                    };
                    user.UserRoles.Add(userRole);
                    _context.SaveChanges();
                }
            }
            return user;
        }

        public UserModel UserRoleRemove(string email, RoleModel role)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                var existingUserRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == role.Id);
                if (existingUserRole != null)
                {
                    user.UserRoles.Remove(existingUserRole);
                    _context.SaveChanges();
                }
            }
            return user;
        }

    }
}
