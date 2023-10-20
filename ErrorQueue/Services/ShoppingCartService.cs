using DataAccess.Models;
using ErrorQueue.DatabaseSettings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ErrorQueue.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IMongoCollection<ShoppingCart> _database;

        public ShoppingCartService(IShoppingCartDatabaseSettings settings,IMongoClient mongoClient) {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _database = database.GetCollection<ShoppingCart>(settings.shooppingCartCollectionName);
        }

        public ShoppingCart CreateNewShoppingCart(ShoppingCart shoppingCart)
        {
            _database.InsertOne(shoppingCart);
            return shoppingCart;    
        }

        public void DeleteShoppingCart(string id)
        {
            _database.DeleteOne(SH => SH.Id == id);
        }

        public ShoppingCart GetShoppingCartById(string id)
        {
            return _database.Find(sh =>sh.Id == id).FirstOrDefault();
        }

        public List<ShoppingCart> GetShoppingCarts()
        {
            return _database.Find(sh => true).ToList();
        }

        public void SendShopping()
        {
            BsonArray shoppingCarts = (BsonArray)_database.ToBson();

        }

        public ShoppingCart SendShopping(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
