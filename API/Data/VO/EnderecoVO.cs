namespace API.Data.VO
{
    public class EnderecoVO
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public ClientVO Client { get; set; }
    }
}
