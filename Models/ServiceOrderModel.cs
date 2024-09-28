using Models.Base;
using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("service_orders")]
    public class ServiceOrderModel : BaseEntity
    {
        [Column("client_id")]
        public int ClientId { get; set; }
        public ClientModel Client { get; set; }
        [Column("equipment_id")]
        public int EquipmentId {  get; set; }
        public EquipmentModel Equipment { get; set; }
        [Column("problem_description")]
        public string ProblemDescription { get; set; } = string.Empty;
        [Column("status")]
        public ServiceOrderStatus Status { get; set; }
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;
        public List<PartModel> Parts { get; set; } = new List<PartModel>();
    }
}
