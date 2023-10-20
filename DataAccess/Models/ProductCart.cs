using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class ProductCart
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("ProductoID")]
        public int ProductoId { get; set; }
        [BsonElement("producto")]
        public Producto Producto { get; set; }
        [BsonElement("cartId")]
        public int CartId { get; set; }
        
        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
