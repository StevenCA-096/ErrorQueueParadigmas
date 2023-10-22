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

        public async void DeleteShoppingCart(string id)
        {
             await _database.DeleteOneAsync(SH => SH.Id == id);
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
            Random rn = new Random();
            var url = "https://localhost:7185/api/ShoppingCart";
   
            using var client = new HttpClient();

            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>(); 
            shoppingCarts = _database.Find(sh => true).ToList();

            foreach (var sh in shoppingCarts) {
                var currentId = sh.Id;
                
                sh.Id =  rn.Next(0, 100).ToString();
                var shoppinCartSerilizared = JsonConvert.SerializeObject(sh);
                var data = new StringContent(shoppinCartSerilizared, Encoding.UTF8, "application/json");
                try
                {
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            await _database.DeleteOneAsync(SH => SH.Id == currentId);
                        }
                        catch (Exception ex){
                            Console.WriteLine(ex);
                        }
                    }
                }
                catch(Exception e){
                    Console.WriteLine(e);
                }
            }
            return shoppingCarts;
            
        }
    }
}
