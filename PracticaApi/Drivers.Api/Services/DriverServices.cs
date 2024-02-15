using Drivers.Api.Configurations;
using Drivers.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace Drivers.Api.Services;


public class DriverServices
{
    private readonly IMongoCollection<Drive> _driversCollection;

    public DriverServices(
        IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionsString);

            var MongoDB=
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _driversCollection= MongoDB.GetCollection<Drive>
            (databaseSettings.Value.ColletionName);
        }
    public async Task<List<Drive>> GetAsync()=>
    await _driversCollection.Find(_ => true).ToListAsync();
   [HttpGet]
   public async Task InsertarDriver(Drive driver)
   {
    await _driversCollection.InsertOneAsync(driver);
   }
 public async Task<Drive> GetDriverId(string id)
   {
    return  await _driversCollection.FindAsync(new BsonDocument{{"_id", new ObjectId(id)}}).Result.FirstAsync();
   }

    public async Task UpdateDriver(Drive driver)
   {
    var filter = Builders<Drive>.Filter.Eq(s=>s.Id, driver.Id);
    await _driversCollection.ReplaceOneAsync(filter, driver);
   }

   public async Task DeleteDriver(string id)
   {
    var filter = Builders<Drive>.Filter.Eq(s=>s.Id,id);
    await _driversCollection.DeleteOneAsync(filter);
   }
}