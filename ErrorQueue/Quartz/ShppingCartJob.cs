using DataAccess.Models;
using ErrorQueue.DatabaseSettings;
using ErrorQueue.Services;
using MongoDB.Driver;
using Quartz;
using Services.IRepository;
using System.Collections.ObjectModel;

namespace ErrorQueue.Quartz
{
    public class ShppingCartJob : IJob
    {
        private readonly IMongoCollection<ShoppingCart> _database;
        private readonly IShoppingCartService _shoppingCartService;

        public ShppingCartJob(IShoppingCartDatabaseSettings settings, IMongoClient mongoClient, IShoppingCartService shoppingCartService)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _database = database.GetCollection<ShoppingCart>(settings.shooppingCartCollectionName);
            this._shoppingCartService = shoppingCartService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _shoppingCartService.SendShopping();
            return Task.CompletedTask;
        }
    }
}
