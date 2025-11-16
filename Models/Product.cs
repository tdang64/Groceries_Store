using Amazon.DynamoDBv2.DataModel;

namespace Group4_Project.Models
{
    [DynamoDBTable("Product")]
    public class Product
    {
        [DynamoDBHashKey]
        public string ProductId { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty]
        public decimal Price { get; set; }

        [DynamoDBProperty]
        public int Stock { get; set; }

        [DynamoDBProperty]
        public string CategoryId { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string SupplierId { get; set; } = string.Empty;
    }
}
