using API.Data.Converter.Implementations;
using API.Data.VO;
using API.Repositories.Generic;
using API.Repositories.ServiceOrder;
using Models;
using Models.Enums;

namespace API.Business.Implementations
{
    public class ServiceOrderBusinessImplementation : IServiceOrderBusiness
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IRepository<PartModel> _repositoryPart;
        private readonly ServiceOrderConverter _converter;
        private readonly PartConverter _converterPart;
        public ServiceOrderBusinessImplementation(IServiceOrderRepository repository, IRepository<PartModel> repositoryPart , ServiceOrderConverter serviceOrderConverter, PartConverter converterPart)
        {
            _repository = repository;
            _repositoryPart = repositoryPart;
            _converter = serviceOrderConverter;
            _converterPart = converterPart;
        }       

        public ServiceOrderVO Create(EditServiceOrderVO serviceOrder)
        {
            var serviceOrderEntity = new ServiceOrderModel
            {
                ClientId = serviceOrder.ClientId,
                EquipmentId = serviceOrder.EquipmentId,
                ProblemDescription = serviceOrder.ProblemDescription,
                Created = DateTime.Now,
                Status = serviceOrder.Status,
            };
            serviceOrderEntity = _repository.Create(serviceOrderEntity);
            return _converter.Parse(serviceOrderEntity);
        }       

        public List<ServiceOrderVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public ServiceOrderVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<ServiceOrderVO> FindByStatus(ServiceOrderStatus status)
        {
            return _converter.Parse(_repository.FindByStatus(status));
        }

        public ServiceOrderVO Update(EditServiceOrderVO serviceOrder)
        {
            if (serviceOrder == null) return null;
            var serviceOrderEntity = _repository.FindById(serviceOrder.Id);
            if (serviceOrderEntity == null) return null;

            serviceOrderEntity.EquipmentId = serviceOrder.EquipmentId;
            serviceOrderEntity.Status = serviceOrder.Status;
            serviceOrderEntity.ProblemDescription = serviceOrder.ProblemDescription;

            return _converter.Parse(_repository.Update(serviceOrderEntity));
        }
        public ServiceOrderVO AddParts(int serviceOrderId, List<PartVO> parts)
        {
            if (parts == null || parts.Count == 0) return null;

            var serviceOrder = FindById(serviceOrderId);
            if (serviceOrder == null) return null;

            foreach (var part in parts)
            {
                part.OsId = serviceOrder.Id;
                var partEntity = _repositoryPart.Create(_converterPart.Parse(part));

                serviceOrder.Parts.Add(_converterPart.Parse(partEntity));
            }

            return serviceOrder;
        }
        public ServiceOrderVO UpdateParts(int serviceOrderId, List<PartVO> parts)
        {
            if (parts == null || parts.Count == 0) return null;

            var serviceOrder = FindById(serviceOrderId);
            if (serviceOrder == null) return null;

            foreach (var part in parts)
            {
                if (_repositoryPart.Exists(part.Id) && serviceOrder.Parts.Exists(p => p.Id == part.Id))
                {
                    var partEntity = _repositoryPart.Update(_converterPart.Parse(part));

                    serviceOrder.Parts.Remove(serviceOrder.Parts.Find(p => p.Id.Equals(part.Id)));

                    serviceOrder.Parts.Add(_converterPart.Parse(partEntity));
                }                
            }
            return serviceOrder;
        }
        public ServiceOrderVO DeleteParts(int serviceOrderId, int partId)
        {
            var serviceOrder = FindById(serviceOrderId);
            if (serviceOrder == null) return null;

            var part = _converterPart.Parse(_repositoryPart.FindByID(partId));

            if(serviceOrder.Parts.Exists(p => p.Id == part.Id))
            {
                _repositoryPart.Delete(partId);
                serviceOrder.Parts.Remove(serviceOrder.Parts.Find(p => p.Id.Equals(part.Id)));

                return serviceOrder;
            }

            return null;
        }
    }
}
