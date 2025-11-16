using Amazon.DynamoDBv2.DataModel;

namespace Group4_Project.Models
{
    [DynamoDBTable("Supplier")]
    public class Supplier
    {
        [DynamoDBHashKey]
        public string SupplierId { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Contact { get; set; } = string.Empty;
    }
}
