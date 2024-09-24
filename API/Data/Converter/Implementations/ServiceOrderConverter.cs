using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class ServiceOrderConverter : IParser<ServiceOrderModel, ServiceOrderVO>, IParser<ServiceOrderVO, ServiceOrderModel>
    {
        public ServiceOrderVO Parse(ServiceOrderModel origin)
        {
            if (origin == null) return null;
            return new ServiceOrderVO
            {
                Id = origin.Id,
                ClientId = origin.ClientId,
                EquipmentId = origin.EquipmentId,
                Status = origin.Status,
                Created = origin.Created,
            };
        }
        public ServiceOrderModel Parse(ServiceOrderVO origin)
        {
            if (origin == null) return null;
            return new ServiceOrderModel
            {
                Id = origin.Id,
                ClientId = origin.ClientId,
                EquipmentId = origin.EquipmentId,
                Status = origin.Status,
                Created = origin.Created,
            };
        }
        public List<ServiceOrderVO> Parse(List<ServiceOrderModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<ServiceOrderModel> Parse(List<ServiceOrderVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
