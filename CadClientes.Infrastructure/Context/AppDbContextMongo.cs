using CadClientes.Domain.Entities;
using MongoDB.Driver;
namespace CadClientes.Infrastructure.Context
{
    public class AppDbContextMongo
    {
        private readonly IMongoDatabase _database;

        private readonly IMongoClient _client;
        public IClientSessionHandle Session { get; private set; }
        public AppDbContextMongo(IMongoClient client, string databaseName)
        {
            _database = client.GetDatabase(databaseName);
            _client = client;
        }

        public void StartSession()
        {
            Session = _client.StartSession();
            Session.StartTransaction();
        }
        public void AbortSession()
        {
            Session.AbortTransaction();
        }
        public IMongoCollection<Cliente> Cliente => _database.GetCollection<Cliente>("Cliente");

        public IMongoCollection<TipoEmpresa> TipoEmpresa => _database.GetCollection<TipoEmpresa>("TipoEmpresa");
    }
}

