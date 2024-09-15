using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class ServiceOrderConverter : IParser<ServiceOrderModel, ServiceOrderVO>, IParser<ServiceOrderVO, ServiceOrderModel>
    {
        private readonly ClientConverter _clientConverter;
        private readonly EquipmentConverter _equipmentConverter;
        private readonly PartConverter _partConverter;
        public ServiceOrderConverter()
        {
            _clientConverter = new ClientConverter();
            _equipmentConverter = new EquipmentConverter();
            _partConverter = new PartConverter();
        }
        public ServiceOrderVO Parse(ServiceOrderModel origin)
        {
            if (origin == null) return null;
            return new ServiceOrderVO
            {
                Id = origin.Id,
                ClientId = origin.ClientId,
                Client = _clientConverter.Parse(origin.Client),
                EquipmentId = origin.EquipmentId,
                Equipment = _equipmentConverter.Parse(origin.Equipment),
                Status = origin.Status,
                Created = origin.Created,
                Parts = _partConverter.Parse(origin.Parts),
            };
        }
        public ServiceOrderModel Parse(ServiceOrderVO origin)
        {
            if (origin == null) return null;
            return new ServiceOrderModel
            {
                Id = origin.Id,
                ClientId = origin.ClientId,
                Client = _clientConverter.Parse(origin.Client),
                EquipmentId = origin.EquipmentId,
                Equipment = _equipmentConverter.Parse(origin.Equipment),
                Status = origin.Status,
                Created = origin.Created,
                Parts = _partConverter.Parse(origin.Parts),
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
