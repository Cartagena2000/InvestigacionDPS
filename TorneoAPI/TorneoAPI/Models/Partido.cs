using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TorneoAPI.Models
{
    public class Partido
    {
        [BsonId]
        public int Id { get; set; }
        public int EquipoLocalId { get; set; }
        public int EquipoVisitanteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Resultado { get; set; }
    }
}