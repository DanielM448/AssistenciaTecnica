using Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("equipments")]
    public class EquipmentModel : BaseEntity
    {
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        [Column("model")]
        public string  ModelEquipment { get; set; } = string.Empty;
        [Column("serial_number")]
        public string SerialNumber { get; set; } = string.Empty;        
        public List<ServiceOrderModel> ServiceOrders { get; set; } = new List<ServiceOrderModel>();
    }
}
