using DataAccess.Models;

namespace ErrorQueue.Services
{
    public interface IFailedStatusService
    {

        List<FailedStatus> GetFailedCarts();
        FailedStatus GetFailedCartById(string id);
        FailedStatus CreateNewFailedCart(FailedStatus failedStatus);
        FailedStatus SendShopping(FailedStatus failedStatus);
        void DeleteFailedCart(string id);
        void DetectAndSendDuplicateIds(ShoppingCart shoppingCart);
    }
}
