using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseAccess
{
    public class LogRecord
    {
        public ObjectId Id { get; set; }
        public string Log { get; set; }

        public static implicit operator LogRecord(string log) => new LogRecord() { Log = log };
    }

    public class MongoRepo
    {
        private static IMongoClient _client = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase _logDatabase = _client.GetDatabase("NET-Template");
        private static IMongoCollection<LogRecord> _collection = _logDatabase.GetCollection<LogRecord>("Logs");

        public static void InsertLog(string log) => _collection.InsertOne(log);
    }
}
