using Infra.Constants;

namespace Infra.Helper
{
    public class Helper
    {
        public static string GetPostgreDatabaseConnection(string database)
        {
            return
                $"Server={PrebuiltVariables.PostgreHost};" +
                $"Port={PrebuiltVariables.PostgrePort};" +
                $"Userid={PrebuiltVariables.PostgreUser};" +
                $"Password={PrebuiltVariables.PostgrePassword};" +
                $"Timeout={PrebuiltVariables.PostgreTimeout};" +
                $"Database={database}";
        }

        public static string GetMongoDatabaseConnection()
        {
            return
                $"mongodb://{PrebuiltVariables.MongoUser}:" +
                $"{PrebuiltVariables.MongoPassword}@" +
                $"{PrebuiltVariables.MongoHost}:" +
                $"{PrebuiltVariables.MongoPort}";
        }
    }
}
