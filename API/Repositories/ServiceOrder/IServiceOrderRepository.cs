using Models;
using Models.Enums;

namespace API.Repositories.ServiceOrder
{
    public interface IServiceOrderRepository
    {
        ServiceOrderModel Create(ServiceOrderModel model);
        ServiceOrderModel Update(ServiceOrderModel model);
        ServiceOrderModel FindById(int id);
        List<ServiceOrderModel> FindAll();
        List<ServiceOrderModel> FindByStatus(ServiceOrderStatus status);

        bool Exists (int id);
    }
}
