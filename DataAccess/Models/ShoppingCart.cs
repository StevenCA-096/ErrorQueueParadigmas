
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
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("cartId")]
        public string CartId { get; set; }
        [BsonElement("customerId")]
        public string CustomerId { get; set; }
        
        [BsonElement("products")]
        public List<Producto> products { get; set; } = new List<Producto>();
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("total")]
        public double Total { get; set; }
        [BsonElement("subtotal")]
        public double SubTotal { get; set; }
        
           

    }
}
