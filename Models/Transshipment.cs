namespace TeloApi.Models
{
    public class Transshipment
    {
        public Guid Id { get; set; }
        public string ShipmentId { get; set; }
        public string Status { get; set; }
    }
}