
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
        //[BsonRepresentation(BsonType.ObjectId)]
        public string CartId { get; set; }
        [BsonElement("costumerId")]
        public int CostumerId { get; set; }
        
        [BsonElement("products")]
        public List<Producto> products { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("subtotal")]
        public double SubTotal { get; set; }
        [BsonElement("total")]
        public double Total { get; set; }
           

    }
}
