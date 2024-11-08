namespace Service.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public  List<OrderDetailsDTO> Details { get; set; }
    }
}
