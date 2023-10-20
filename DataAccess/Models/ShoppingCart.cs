
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class ShoppingCart
    {   
        [BsonId]
        public string Id { get; set; }
        [BsonElement("userId")]
        public int UserId { get; set; }
        [BsonElement("subtotal")]
        public double SubTotal { get; set; }
        [BsonElement("total")]
        public double Total { get; set; }
        [BsonElement("shoppingDate")]
        public DateTime ShoppingDate { get; set; }
        [BsonIgnore]
        public List<ProductCart> ProductCarts { get; set; } = new List<ProductCart>();

    }
}
