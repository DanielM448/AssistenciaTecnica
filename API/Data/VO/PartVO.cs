namespace API.Data.VO
{
    public class PartVO
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OsId { get; set; }
        public ServiceOrderVO Order { get; set; }
    }
}
