using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class UserConverter : IParser<UserVO, UserModel>, IParser<UserModel, UserVO>
    {
        public UserModel Parse(UserVO origin)
        {
            if (origin == null) return null;
            return new UserModel
            {
                Name = origin.UserName,
                Email = origin.Email,
            };
        }

        public UserVO Parse(UserModel origin)
        {
            if (origin == null) return null;
            return new UserVO
            {
                UserName = origin.Name,
                Email = origin.Email,
            };
        }

        public List<UserVO> Parse(List<UserModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<UserModel> Parse(List<UserVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
