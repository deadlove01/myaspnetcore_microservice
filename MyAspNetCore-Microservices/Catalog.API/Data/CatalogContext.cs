using Catalog.API.Configs;
using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);

        Products = db.GetCollection<Product>(settings.CollectionName);
        CatalogContextSeedData.SeedData(Products);
    }
    
    public IMongoCollection<Product> Products { get; }
}