using Drivers.Api.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Drivers.Api.Configuration;
using MongoDB.Bson;

namespace Drivers.Api.DriverServices;

public class DriverServices
{
    private readonly IMongoCollection<Driver> _driversCollection;

    public DriverServices(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _driversCollection = mongoDB.GetCollection<Driver>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<Driver>> GetAsync() => await _driversCollection.Find(_ => true).ToListAsync();

    public async Task<Driver> GetDriverById(string Id)
    {
        return await _driversCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
    }

    public async Task InsertDriver(Driver driver)
    {
        await _driversCollection.InsertOneAsync(driver);
    }

    public async Task UpdateDriver(Driver driver)
    {
        var filter = Builders<Driver>.Filter.Eq(s => s.Id, driver.Id);
        await _driversCollection.ReplaceOneAsync(filter, driver);
    }

    public async Task DeleteDriver(string Id)
    {
        var filter = Builders<Driver>.Filter.Eq(s => s.Id, Id);
        await _driversCollection.DeleteOneAsync(filter);
    }
}