using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class EquipmentConverter : IParser<EquipmentVO, EquipmentModel>, IParser<EquipmentModel, EquipmentVO>
    {
        public EquipmentModel Parse(EquipmentVO origin)
        {
            if (origin == null) return null;
            return new EquipmentModel
            {
                Id = origin.Id,
                Brand = origin.Brand,
                ModelEquipment = origin.ModelEquipment,
                SerialNumber = origin.SerialNumber,
                ProblemDescription = origin.ProblemDescription,
            };
        }
        public EquipmentVO Parse(EquipmentModel origin)
        {
            if (origin == null) return null;
            return new EquipmentVO
            {
                Id = origin.Id,
                Brand = origin.Brand,
                ModelEquipment = origin.ModelEquipment,
                SerialNumber = origin.SerialNumber,
                ProblemDescription = origin.ProblemDescription,
            };
        }
        public List<EquipmentModel> Parse(List<EquipmentVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<EquipmentVO> Parse(List<EquipmentModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
