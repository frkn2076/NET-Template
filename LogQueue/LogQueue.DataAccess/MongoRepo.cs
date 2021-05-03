using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace LogQueue.DataAccess
{
    public class MongoRepo
    {
        private static readonly IMongoClient _client = GetMongoClient();
        private static readonly string _logDatabaseName = Environment.GetEnvironmentVariable("MongoLogDB");
        private static readonly IMongoDatabase _logDatabase = _client.GetDatabase(_logDatabaseName);
        private static IMongoClient GetMongoClient()
        {
            var user = Environment.GetEnvironmentVariable("MongoLogUser");
            var password = Environment.GetEnvironmentVariable("MongoLogPassword");
            var host = Environment.GetEnvironmentVariable("MongoLogHost");
            var port = Environment.GetEnvironmentVariable("MongoLogPort");
            var client = new MongoClient($"mongodb://{user}:{password}@{host}:{port}");
            return client;
        }
        private static IMongoCollection<BsonDocument> _collection = _logDatabase.GetCollection<BsonDocument>("Logs");

        public static void InsertLog(string log) => _collection.InsertOne(BsonDocument.Parse(log));
    }
}
