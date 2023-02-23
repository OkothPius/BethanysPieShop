namespace BethanysPieShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Zipcode { get;set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set;}
        public string? PhoneNumber { get; set; }
        public string? Email { get; set;}
        public decimal OrderTotal { get; set;}
        public DateTime OrderPlaced { get; set;}
    }
}
