using CRUDMongoDb.Models;
using MongoDB.Driver;
using System;

namespace CRUDMongoDb.Context
{
    public class MongoContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }
        public MongoContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }
        public IMongoCollection<Pessoa> pessoas
        {
            get
            {
                return _database.GetCollection<Pessoa>("Pessoas");
            }
        }
    }
}
