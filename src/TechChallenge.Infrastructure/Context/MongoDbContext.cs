using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TechChallenge.Infrastructure.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("TechChallengeDb");
    }

    public IMongoDatabase Database => _database;
}