using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TorneoAPI.Models
{
    public class Equipo
    {
        [BsonId]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Jugador> Jugadores { get; set; }
    }
 }