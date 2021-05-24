using Infra.Constants;
using Infra.Helper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogQueue.DataAccess
{
    public static class MongoRepo
    {
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _logDatabase;
        private static readonly IMongoCollection<BsonDocument> _logCollection;
        static MongoRepo()
        {
            var mongoDatabaseConnection = Helper.GetMongoDatabaseConnection();
            _client = new MongoClient(mongoDatabaseConnection);
            _logDatabase = _client.GetDatabase(PrebuiltVariables.MongoLogDB);
            _logCollection = _logDatabase.GetCollection<BsonDocument>("Logs");
        }

        public static void InsertLog(string log) =>_logCollection.InsertOne(BsonDocument.Parse(log));
    }
}
