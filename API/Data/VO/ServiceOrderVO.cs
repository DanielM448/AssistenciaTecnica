using Models.Enums;

namespace API.Data.VO
{
    public class ServiceOrderVO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ClientVO Client { get; set; }
        public int EquipmentId { get; set; }
        public EquipmentVO Equipment { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public DateTime Created {  get; set; }
        public List<PartVO> Parts { get; set; }
    }
}
