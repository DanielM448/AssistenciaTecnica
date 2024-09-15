namespace API.Data.VO
{
    public class EquipmentVO
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string ModelEquipment { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string ProblemDescription { get; set; } = string.Empty;
    }
}
