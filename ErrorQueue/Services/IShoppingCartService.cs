using DataAccess.Models;

namespace ErrorQueue.Services
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetShoppingCarts();
        ShoppingCart GetShoppingCartById(int id);
        ShoppingCart CreateNewShoppingCart(ShoppingCart shoppingCart);
        ShoppingCart SendShopping(ShoppingCart shoppingCart);
        void DeleteShoppingCart(int id);
    }
}
