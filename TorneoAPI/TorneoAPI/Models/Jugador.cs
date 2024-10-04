using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TorneoAPI.Models
{
    public class Jugador
    {
        [BsonId]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int EquipoId { get; set; }
    }
}
