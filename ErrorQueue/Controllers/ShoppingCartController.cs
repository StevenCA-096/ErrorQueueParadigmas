using DataAccess.Models;
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


        [HttpGet]
        public IEnumerable<ShoppingCart> Get()
        {
            return _shoppingCartRepository.GetAll();
        }
        // POST api/<ShoppingCartController>
        [HttpPost]
        public void Post([FromBody] ShoppingCart shoppingCart)
        {
            _shoppingCartRepository.Insert(shoppingCart);
            _shoppingCartRepository.Save();

        }

        // PUT api/<ShoppingCartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShoppingCartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
