using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TorneoAPI.Models
{
    public class Resultado
    {
        [BsonId]
     
        public int PartidoId { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public string Observaciones { get; set; }
    }
}
