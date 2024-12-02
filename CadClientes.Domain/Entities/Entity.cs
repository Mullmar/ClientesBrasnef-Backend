using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CadClientes.Domain.Entities
{
    public abstract class Entity
    {
        [BsonId]  // identificador único do documento
        [NotMapped]
        public ObjectId ObjectId { get; set; }

        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}
