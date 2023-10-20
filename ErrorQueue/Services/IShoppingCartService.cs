using DataAccess.Models;

namespace ErrorQueue.Services
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetShoppingCarts();
        ShoppingCart GetShoppingCartById(string id);
        ShoppingCart CreateNewShoppingCart(ShoppingCart shoppingCart);
        public Task<List<ShoppingCart>> SendShopping();
        void DeleteShoppingCart(string id);
    }
}
