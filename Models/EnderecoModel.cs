using Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("enderecos")]
    public class EnderecoModel : BaseEntity
    {        
        [Column("Street")]
        public string Street { get; set; } = string.Empty;
        [Column("number")]
        public int Number { get; set; }
        [Column("complement")]
        public string Complement { get; set; } = string.Empty;
        [Column("neighborhood")]
        public string Neighborhood { get; set; } = string.Empty;
        [Column("city")]
        public string City { get; set; } = string.Empty;
        [Column("state")]
        public string State { get; set; } = string.Empty;
        [Column("zip_code")]
        public string ZipCode { get; set; } = string.Empty;

        [Column("client_id")]
        public int ClientId { get; set; }
        public  ClientModel Client { get; set; }
    }
}
