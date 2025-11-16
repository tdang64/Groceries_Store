using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace Group4_Project.Services
{
    public class DynamoDbService
    {
        private readonly AmazonDynamoDBClient _client;

        public DynamoDbService()
        {
            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1 // đổi region bạn dùng
            };

            _client = new AmazonDynamoDBClient(config);
        }

        public AmazonDynamoDBClient Client => _client;
    }
}
