
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class Producto
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string productId { get; set; }
        [BsonElement("name")]
        public string productName { get; set; }
        [BsonElement("price")]
        public double productPrice { get; set; }
        [BsonElement("productQuantity")]
        public int productQuantity { get; set; }
        //[BsonIgnore]
        //public List<ProductCart>? ProductsCart { get; set; }
    }
}
