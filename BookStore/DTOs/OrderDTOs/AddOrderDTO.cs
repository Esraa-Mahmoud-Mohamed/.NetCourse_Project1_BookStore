namespace BookStore.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public string cust_id { get; set; }
        public List<AddDetailDTO> books { get; set; } = new List<AddDetailDTO>();
    }
}
