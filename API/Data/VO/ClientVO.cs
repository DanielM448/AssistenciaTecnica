namespace API.Data.VO
{
    public class ClientVO
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string CellNumber { get; set; } = string.Empty;
        public string CellNumberAlternative { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<EnderecoVO> Enderecos { get; set; } = new List<EnderecoVO>();
        public List<ServiceOrderVO> ServiceOrders { get; set; } = new List<ServiceOrderVO>();
    }
}
