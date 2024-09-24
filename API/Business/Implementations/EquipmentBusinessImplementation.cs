using API.Data.Converter.Implementations;
using API.Data.VO;
using API.Repositories.Generic;
using Models;

namespace API.Business.Implementations
{
    public class EquipmentBusinessImplementation : IEquipmentBusiness
    {
        private readonly IRepository<EquipmentModel> _repository;
        private readonly EquipmentConverter _converter;
        public EquipmentBusinessImplementation(IRepository<EquipmentModel> repository, EquipmentConverter converter)
        {
            _repository = repository;
            _converter = converter;
        }
        public EquipmentVO Create(EquipmentVO equipment)
        {
            var equipmentEntity = _converter.Parse(equipment);
            equipmentEntity = _repository.Create(equipmentEntity);
            return _converter.Parse(equipmentEntity);
        }

        public List<EquipmentVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public EquipmentVO FindByID(int id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public EquipmentVO Update(EquipmentVO equipment)
        {
            var equipmentEntity = _converter.Parse(equipment);
            equipmentEntity = _repository.Update(equipmentEntity);
            return _converter.Parse(equipmentEntity);
        }
    }
}
