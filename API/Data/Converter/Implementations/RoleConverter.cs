using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class RoleConverter : IParser<RoleVO, RoleModel>, IParser<RoleModel, RoleVO>
    {
        public RoleModel Parse(RoleVO origin)
        {
            if (origin == null) return null;
            return new RoleModel
            {
                Id = origin.Id,
                RoleName = origin.Name,
            };
        }

        public RoleVO Parse(RoleModel origin)
        {
            if (origin == null) return null;
            return new RoleVO
            {
                Id = origin.Id,
                Name = origin.RoleName,
            };
        }

        public List<RoleVO> Parse(List<RoleModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<RoleModel> Parse(List<RoleVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
