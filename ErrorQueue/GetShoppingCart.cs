using Newtonsoft.Json;
using Quartz;
using System.Text;

namespace ErrorQueue
{
    public class NumberGeneratorJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using var client = new HttpClient();

            var url = "https://localhost:7077/api/Event";
            using var client = new HttpClient();
            newCart.ProductCarts.ForEach(x => x.ShoppingCart = null);

            var json = JsonConvert.SerializeObject(newCart);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            //var response = await client.PostAsync(url, data);

            //var json = JsonConvert.SerializeObject(null);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            //var url = "https://localhost:7185/api/notification/";

            try
            {
                var response = await client.PostAsync(url, data);
            }
            catch (Exception ex)
            {

                throw;
            }

            await Task.CompletedTask;
        }

        private int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
