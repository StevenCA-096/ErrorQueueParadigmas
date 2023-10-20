using DataAccess.Models;
using ErrorQueue.DatabaseSettings;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text;
using static System.Net.WebRequestMethods;

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

        public async Task<List<ShoppingCart>> SendShopping()
        {
            //"https://localhost:7077/api/Event"
            var url = "https://localhost:7185/api/ShoppingCart";
   
            using var client = new HttpClient();

            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            shoppingCarts = _database.Find(sh => true).ToList();

            foreach (var sh in shoppingCarts) {
                var shoppinCartSerilizared = JsonConvert.SerializeObject(sh);
                var data = new StringContent(shoppinCartSerilizared, Encoding.UTF8, "application/json");
                try
                {
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        DeleteShoppingCart(sh.Id);
                    }
                }
                catch(Exception e){
                    throw e;
                }
            }
            return shoppingCarts;
            
        }
    }
}
