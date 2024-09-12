using API.Data.VO;

namespace API.Business
{
    public interface IRoleBusiness
    {
        RoleVO FindByID(int id);
        List<RoleVO> FindAll();
    }
}
