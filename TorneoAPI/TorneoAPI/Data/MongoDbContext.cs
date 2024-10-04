
using MongoDB.Driver;
using TorneoAPI.Models;

namespace TorneoAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("torneo_futbol");
        }

        public IMongoCollection<Equipo> Equipos => _database.GetCollection<Equipo>("Equipos");
        public IMongoCollection<Jugador> Jugadores => _database.GetCollection<Jugador>("Jugadores");
        public IMongoCollection<Partido> Partidos => _database.GetCollection<Partido>("Partidos");
        public IMongoCollection<Resultado> Resultados => _database.GetCollection<Resultado>("Resultados");

    }
}
