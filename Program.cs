using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Group4_Project.Repository.Implementations;
using Group4_Project.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Read AWS config

var aws = builder.Configuration.GetSection("AWS");
var credentials = new BasicAWSCredentials(
    aws["AccessKey"],
    aws["SecretKey"]
);

var region = RegionEndpoint.GetBySystemName(aws["Region"]);


// Register AWS DynamoDB client

builder.Services.AddSingleton<IAmazonDynamoDB>(
    new AmazonDynamoDBClient(credentials, region)
);


// Register DynamoDB Context

builder.Services.AddScoped<IDynamoDBContext>(sp =>
{
    var client = sp.GetRequiredService<IAmazonDynamoDB>();
    return new DynamoDBContext(client);
});

// Repositories

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Controllers
builder.Services.AddControllers().AddNewtonsoftJson();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
