using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Domain.Entities
{
    public sealed class TipoEmpresa
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}
