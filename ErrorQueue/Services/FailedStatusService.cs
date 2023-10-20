using DataAccess.Models;
using ErrorQueue.DatabaseSettings;
using MongoDB.Driver;

namespace ErrorQueue.Services
{
    public class FailedStatusService : IFailedStatusService
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IQueueService _queueService;
        private readonly IMongoCollection<FailedStatus> _database;

        public FailedStatusService(IFailedStatusDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _database = database.GetCollection<FailedStatus>(settings.failedStatusCollectionName);
        }

        public FailedStatusService(IShoppingCartService shoppingCartService, IQueueService queueService)
        {
            _shoppingCartService = shoppingCartService;
            _queueService = queueService;
        }

        public FailedStatus GetFailedCartById(string id)
        {
            return _database.Find(sh => sh.Id == id).FirstOrDefault();
        }

        public List<FailedStatus> GetFailedCarts()
        {
            return _database.Find(sh => true).ToList();
        }

        public void DeleteFailedCart(string id)
        {
            _database.DeleteOne(SH => SH.Id == id);
        }

        public void DetectAndSendDuplicateIds(ShoppingCart shoppingCart)
        {

            var failedShoppingCarts = _shoppingCartService.GetShoppingCarts().Where(sc => sc.Id == shoppingCart.Id).ToList();

            if (failedShoppingCarts.Count > 1)
            {
                foreach (var failerShoppingCart in failedShoppingCarts)
                {
                    _queueService.Enqueue(failerShoppingCart);
                }
            }
        }
    }
}
