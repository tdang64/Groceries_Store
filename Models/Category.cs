using Amazon.DynamoDBv2.DataModel;

namespace Group4_Project.Models
{
    [DynamoDBTable("Category")]
    public class Category
    {
        [DynamoDBHashKey]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;
    }
}
