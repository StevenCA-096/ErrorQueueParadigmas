using DataAccess.Models;
using ErrorQueue.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ErrorQueue.Controllers
{
    public class FailedStatusController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;
        private readonly IQueueService _queueService;

        public FailedStatusController(IShoppingCartService shoppingCartService, IQueueService queueService)
        {
            _shoppingCartService = shoppingCartService;
            _queueService = queueService;
        }

        [HttpGet]
        public IEnumerable<ShoppingCart> Get()
        {
            return _shoppingCartService.GetShoppingCarts();
        }

        [HttpPost]
        public void ReceiveAndSendDuplicateIds([FromBody] List<FailedStatus> failedStatuses)
        {

            foreach (var failedStatus in failedStatuses)
            {
                 _queueService.Enqueue(failedStatus.Id);  
            }
        }
    }
}
