using Models.Enums;

namespace API.Data.VO
{
    public class EditServiceOrderVO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EquipmentId { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
        public ServiceOrderStatus Status { get; set; }
    }
}
