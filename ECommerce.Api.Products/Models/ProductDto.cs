namespace ECommerce.Api.Products.Models
{
    /**
     * This model is the DTO
     */
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}