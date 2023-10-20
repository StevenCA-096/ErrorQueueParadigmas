using DataAccess.Models;

namespace ErrorQueue.Services
{
    public interface IFailedStatusService
    {
        void DetectAndSendDuplicateIds(ShoppingCart shoppingCart);
    }
}
