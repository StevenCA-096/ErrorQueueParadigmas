using DataAccess.Models;

namespace ErrorQueue.Services
{
    public class FailedStatusService : IFailedStatusService
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IQueueService _queueService;

        public FailedStatusService(IShoppingCartService shoppingCartService, IQueueService queueService)
        {
            _shoppingCartService = shoppingCartService;
            _queueService = queueService;
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
