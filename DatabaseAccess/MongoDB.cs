using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace DatabaseAccess
{
    public class MongoRepo
    {
        private static string _mongoUser = Environment.GetEnvironmentVariable("MongoUser");
        private static string _mongoPassword = Environment.GetEnvironmentVariable("MongoPassword");
        private static IMongoClient _client = new MongoClient($"mongodb://{_mongoUser}:{_mongoPassword}@localhost:27017");
        private static IMongoDatabase _logDatabase = _client.GetDatabase("LOGDB");
        private static IMongoCollection<BsonDocument> _collection = _logDatabase.GetCollection<BsonDocument>("Logs");

        public static void InsertLog(string log) => _collection.InsertOne(BsonDocument.Parse(log));
    }
}
