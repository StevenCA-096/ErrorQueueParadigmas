using DataAccess.Models;
using ErrorQueue.Services;
using Microsoft.AspNetCore.Mvc;
using Services.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ErrorQueue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService) {
            this._shoppingCartService = shoppingCartService;
        }


        [HttpGet]
        public IEnumerable<ShoppingCart> Get()
        {
            return _shoppingCartService.GetShoppingCarts();
        }

        [HttpGet("id")]
        public ShoppingCart GetById(string id)
        {
            return _shoppingCartService.GetShoppingCartById(id);
        }
        // POST api/<ShoppingCartController>
        [HttpPost]
        public ActionResult<ShoppingCart> Post([FromBody] ShoppingCart shoppingCart)
        {
            _shoppingCartService.CreateNewShoppingCart(shoppingCart);
            return CreatedAtAction(nameof(GetById), new { id = shoppingCart.Id }, shoppingCart);
            
            //_shoppingCartRepository.Insert(shoppingCart);
            //_shoppingCartRepository.Save();
           
        }

        // PUT api/<ShoppingCartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShoppingCartController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _shoppingCartService.DeleteShoppingCart(id);
        }
    }
}
