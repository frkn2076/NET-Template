using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace LogQueue.DataAccess
{
    public static class MongoRepo
    {
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _logDatabase;
        private static readonly IMongoCollection<BsonDocument> _logCollection;
        static MongoRepo()
        {
            var user = Environment.GetEnvironmentVariable("MongoLogUser");
            var password = Environment.GetEnvironmentVariable("MongoLogPassword");
            var host = Environment.GetEnvironmentVariable("MongoLogHost");
            var port = Environment.GetEnvironmentVariable("MongoLogPort");
            _client = new MongoClient($"mongodb://{user}:{password}@{host}:{port}");

            var logDatabaseName = Environment.GetEnvironmentVariable("MongoLogDB");
            _logDatabase = _client.GetDatabase(logDatabaseName);

            _logCollection = _logDatabase.GetCollection<BsonDocument>("Logs");
        }

        public static void InsertLog(string log) => _logCollection.InsertOne(BsonDocument.Parse(log));
    }
}
