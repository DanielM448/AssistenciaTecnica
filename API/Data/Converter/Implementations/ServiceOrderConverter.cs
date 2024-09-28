using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class ServiceOrderConverter : IParser<ServiceOrderModel, ServiceOrderVO>, IParser<ServiceOrderVO, ServiceOrderModel>
    {
        private readonly PartConverter _converter;
        public ServiceOrderConverter(PartConverter converter)
        {
            _converter = converter;
        }

        public ServiceOrderVO Parse(ServiceOrderModel origin)
        {
            if (origin == null) return null;
            return new ServiceOrderVO
            {
                Id = origin.Id,
                ClientId = origin.ClientId,
                EquipmentId = origin.EquipmentId,
                ProblemDescription = origin.ProblemDescription,
                Status = origin.Status,
                Created = origin.Created,
                Parts = _converter.Parse(origin.Parts)
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
                ProblemDescription = origin.ProblemDescription,
                Status = origin.Status,
                Created = origin.Created,
                Parts = _converter.Parse(origin.Parts)
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
