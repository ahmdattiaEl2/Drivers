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
    }
}
