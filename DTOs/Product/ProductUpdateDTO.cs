namespace Group4_Project.DTOs
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string CategoryId { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
    }
}
