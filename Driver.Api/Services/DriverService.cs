using Drivers.Api.Configurations;
using Drivers.Api.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Drivers.Api.Services
{
    public class DriverService
    {
        private IMongoCollection<Driver> _driverCollection;
        public DriverService(IOptions<DataBaseSettings> _databaseSettings)
        {
            var mongoClient = new MongoClient(_databaseSettings.Value.connectionString); //mongoDb client to communicat to the database
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName); //the database
            _driverCollection = mongoDb.GetCollection<Driver>(_databaseSettings.Value.CollectionName); // the collection to work with
        }

        public async Task<List<Driver>> GetAsync() => await _driverCollection.Find(_ => true).ToListAsync();

        public async Task<Driver> GetAsync(string id) => await _driverCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Driver driver) => await _driverCollection.InsertOneAsync(driver);

        public async Task UpdateAsync(string id, Driver driver) => await _driverCollection.ReplaceOneAsync(x => x.Id == id, driver);

        public async Task RemoveAsync(string id) => await _driverCollection.DeleteOneAsync(x => x.Id == id);


    }
}
