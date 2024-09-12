using Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("clients")]
    public class ClientModel : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("cell_number")]
        public string CellNumber { get; set; } = string.Empty;
        [Column("cell_number_alternative")]
        public string CellNumberAlternative { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        public List<EnderecoModel> Enderecos { get; set; } = new List<EnderecoModel>();
        public List<ServiceOrderModel> ServiceOrders { get; set; } = new List<ServiceOrderModel>();
    }
}
