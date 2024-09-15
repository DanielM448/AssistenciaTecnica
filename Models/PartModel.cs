using Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("parts")]
    public class PartModel : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public double Price {  get; set; }
        [Column("os_id")]
        public int OsId { get; set; }
        [ForeignKey("OsId")]
        public ServiceOrderModel Order { get; set; }
    }
}
