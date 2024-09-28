using API.Data.VO;
using Models.Enums;

namespace API.Business
{
    public interface IServiceOrderBusiness
    {
        ServiceOrderVO Create(EditServiceOrderVO serviceOrder);
        ServiceOrderVO Update(EditServiceOrderVO serviceOrder);
        ServiceOrderVO FindById(int id);
        List<ServiceOrderVO> FindAll();
        List<ServiceOrderVO> FindByStatus(ServiceOrderStatus status);
        ServiceOrderVO AddParts(int serviceOrderId, List<PartVO> parts);
        ServiceOrderVO UpdateParts(int serviceOrderId, List<PartVO> parts);
        ServiceOrderVO DeleteParts(int serviceOrderId, int partId);
    }
}
