using API.Data.VO;

namespace API.Business
{
    public interface IEquipmentBusiness
    {
        EquipmentVO Create(EquipmentVO equipment);
        EquipmentVO FindByID(int id);
        List<EquipmentVO> FindAll();
        EquipmentVO Update(EquipmentVO equipment);

    }
}
