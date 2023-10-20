
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
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("price")]
        public int Price { get; set; }
        //[BsonIgnore]
        //public List<ProductCart>? ProductsCart { get; set; }
    }
}
